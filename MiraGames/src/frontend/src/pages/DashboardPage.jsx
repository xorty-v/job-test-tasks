import { useState, useEffect, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import ClientSection from "../components/ClientSection";
import RateSection from "../components/RateSection";
import PaymentsSection from "../components/PaymentsSection";
import { fetchWithAuth } from "../services/authService.js";

function DashboardPage() {
  const [clients, setClients] = useState([]);
  const [rate, setRate] = useState(null);
  const [payments, setPayments] = useState([]);

  const [loadingClients, setLoadingClients] = useState(true);
  const [loadingRate, setLoadingRate] = useState(true);
  const [loadingPayments, setLoadingPayments] = useState(true);

  const [clientsError, setClientsError] = useState("");
  const [rateError, setRateError] = useState("");
  const [paymentsError, setPaymentsError] = useState("");

  const navigate = useNavigate();

  const handleUnauthorized = useCallback(() => {
    console.warn("Неавторизованный доступ. Перенаправление на страницу входа.");
    setClientsError("Сессия истекла. Пожалуйста, войдите снова.");
    setRateError("Сессия истекла. Пожалуйста, войдите снова.");
    setPaymentsError("Сессия истекла. Пожалуйста, войдите снова.");
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    navigate("/login");
  }, [navigate]);

  const fetchClients = useCallback(async () => {
    setLoadingClients(true);
    setClientsError("");
    try {
      const response = await fetchWithAuth("/clients", {}, handleUnauthorized);

      if (!response.ok) {
        const errorData = await response
          .json()
          .catch(() => ({ message: "Неизвестная ошибка" }));
        setClientsError(
          `Не удалось загрузить клиентов: ${
            errorData.message || response.statusText
          }`
        );
        return;
      }
      const data = await response.json();
      setClients(data);
    } catch (err) {
      setClientsError("Ошибка загрузки клиентов: " + err.message);
    } finally {
      setLoadingClients(false);
    }
  }, [handleUnauthorized]);

  const fetchRate = useCallback(async () => {
    setLoadingRate(true);
    setRateError("");
    try {
      const response = await fetchWithAuth("/rate", {}, handleUnauthorized);

      if (!response.ok) {
        const errorData = await response
          .json()
          .catch(() => ({ message: "Неизвестная ошибка" }));
        setRateError(
          `Не удалось загрузить курс: ${
            errorData.message || response.statusText
          }`
        );
        return;
      }
      const data = await response.json();
      const parsedRate = parseFloat(data);
      if (!isNaN(parsedRate)) {
        setRate(parsedRate);
      } else {
        setRateError("Некорректный формат курса токенов.");
        setRate(0);
      }
    } catch (err) {
      setRateError("Ошибка загрузки курса: " + err.message);
    } finally {
      setLoadingRate(false);
    }
  }, [handleUnauthorized]);

  const fetchPayments = useCallback(async () => {
    setLoadingPayments(true);
    setPaymentsError("");
    try {
      const response = await fetchWithAuth(
        "/payments?take=5",
        {},
        handleUnauthorized
      );

      if (!response.ok) {
        const errorData = await response
          .json()
          .catch(() => ({ message: "Неизвестная ошибка" }));
        setPaymentsError(
          `Не удалось загрузить платежи: ${
            errorData.message || response.statusText
          }`
        );
        return;
      }
      const data = await response.json();
      setPayments(data);
    } catch (err) {
      setPaymentsError("Ошибка загрузки платежей: " + err.message);
    } finally {
      setLoadingPayments(false);
    }
  }, [handleUnauthorized]);

  useEffect(() => {
    fetchClients();
    fetchRate();
    fetchPayments();
  }, [fetchClients, fetchRate, fetchPayments]);

  const handleRateChange = useCallback(
    async (newRateValue) => {
      setRateError("");
      try {
        const response = await fetchWithAuth(
          "/rate",
          {
            method: "POST",
            body: JSON.stringify({ value: newRateValue }),
          },
          handleUnauthorized
        );

        if (!response.ok) {
          const errorData = await response
            .json()
            .catch(() => ({ message: "Неизвестная ошибка" }));
          setRateError(
            `Не удалось обновить курс: ${
              errorData.message || response.statusText
            }`
          );
          return;
        }
        setRate(newRateValue);
      } catch (err) {
        setRateError("Ошибка обновления курса: " + err.message);
      }
    },
    [handleUnauthorized]
  );

  const handleAddClient = useCallback(
    async (name, email) => {
      setClientsError("");
      try {
        const response = await fetchWithAuth(
          "/clients",
          {
            method: "POST",
            body: JSON.stringify({ name, email }),
          },
          handleUnauthorized
        );

        if (!response.ok) {
          const errorData = await response
            .json()
            .catch(() => ({ message: "Неизвестная ошибка" }));
          setClientsError(
            `Не удалось добавить клиента: ${
              errorData.message || response.statusText
            }`
          );
          return;
        }
        fetchClients();
      } catch (err) {
        setClientsError("Ошибка добавления клиента: " + err.message);
      }
    },
    [fetchClients, handleUnauthorized]
  );

  const handleUpdateClient = useCallback(
    async (id, name, email, balanceT) => {
      setClientsError("");
      try {
        const response = await fetchWithAuth(
          `/clients/${id}`,
          {
            method: "PUT",
            body: JSON.stringify({ name, email, balanceT }),
          },
          handleUnauthorized
        );

        if (!response.ok) {
          const errorData = await response
            .json()
            .catch(() => ({ message: "Неизвестная ошибка" }));
          setClientsError(
            `Не удалось обновить клиента: ${
              errorData.message || response.statusText
            }`
          );
          return;
        }

        setClients((prevClients) =>
          prevClients.map((client) =>
            client.id === id ? { ...client, name, email, balanceT } : client
          )
        );
      } catch (err) {
        setClientsError("Ошибка обновления клиента: " + err.message);
      }
    },
    [handleUnauthorized]
  );

  const handleDeleteClient = useCallback(
    async (clientId) => {
      setClientsError("");
      if (!window.confirm("Уверены, что хотите удалить этого клиента?")) {
        return;
      }

      try {
        const response = await fetchWithAuth(
          `/clients/${clientId}`,
          {
            method: "DELETE",
          },
          handleUnauthorized
        );

        if (!response.ok) {
          const errorData = await response
            .json()
            .catch(() => ({ message: "Неизвестная ошибка" }));
          setClientsError(
            `Не удалось удалить клиента: ${
              errorData.message || response.statusText
            }`
          );
          return;
        }

        setClients((prevClients) =>
          prevClients.filter((client) => client.id !== clientId)
        );
      } catch (err) {
        setClientsError("Ошибка удаления клиента: " + err.message);
      }
    },
    [handleUnauthorized]
  );

  const overallLoading = loadingClients && loadingRate && loadingPayments;
  const hasNoData =
    clients.length === 0 && rate === null && payments.length === 0;

  if (overallLoading && hasNoData) {
    return (
      <div className="container-card">
        <p>Загрузка данных...</p>
      </div>
    );
  }

  return (
    <div className="dashboard-layout">
      <div className="dashboard-main-content">
        <ClientSection
          clients={clients}
          onAddClient={handleAddClient}
          onUpdateClient={handleUpdateClient}
          onDeleteClient={handleDeleteClient}
          loading={loadingClients}
          error={clientsError}
          setError={setClientsError}
        />
      </div>

      <div className="dashboard-sidebar">
        <RateSection
          currentRate={rate}
          onRateChange={handleRateChange}
          loading={loadingRate}
          error={rateError}
          setError={setRateError}
        />
        <PaymentsSection
          payments={payments}
          loading={loadingPayments}
          error={paymentsError}
          setError={setPaymentsError}
        />
      </div>
    </div>
  );
}

export default DashboardPage;
