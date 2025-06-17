import React from "react";
import AddClientModal from "./AddClientModal";
import EditClientModal from "./EditClientModal";

function ClientSection({
  clients,
  onAddClient,
  onUpdateClient,
  onDeleteClient,
  loading,
  error,
  setError,
}) {
  const [showAddClientModal, setShowAddClientModal] = React.useState(false);
  const [editingClient, setEditingClient] = React.useState(null);

  const handleEditClick = (client) => {
    setEditingClient(client);
    setError("");
  };

  const handleCloseEditModal = () => {
    setEditingClient(null);
    setError("");
  };

  const handleCloseAddModal = () => {
    setShowAddClientModal(false);
    setError("");
  };

  return (
    <div className="table-card">
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          marginBottom: "20px",
        }}
      >
        <h3>Клиенты</h3>
        <button
          onClick={() => setShowAddClientModal(true)}
          className="icon-button add"
          title="Добавить клиента"
          disabled={loading}
        >
          &#x2795;
        </button>
      </div>
      {loading ? (
        <p>Загрузка клиентов...</p>
      ) : error ? (
        <p className="error-message">{error}</p>
      ) : clients.length > 0 ? (
        <table className="data-table">
          <thead>
            <tr>
              <th>Name</th>
              <th>Email</th>
              <th>BalanceT</th>
              <th>Действия</th>
            </tr>
          </thead>
          <tbody>
            {clients.map((client) => (
              <tr key={client.id}>
                <td>{client.name}</td>
                <td>{client.email}</td>
                <td>
                  {typeof client.balanceT === "number"
                    ? client.balanceT.toFixed(2)
                    : "N/A"}
                </td>
                <td>
                  <button
                    onClick={() => handleEditClick(client)}
                    className="icon-button edit"
                    title="Редактировать"
                    disabled={loading}
                  >
                    &#x270E;
                  </button>
                  <button
                    onClick={() => onDeleteClient(client.id)}
                    className="icon-button delete"
                    title="Удалить"
                    disabled={loading}
                  >
                    &#x1F5D1;
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>Клиенты не найдены.</p>
      )}

      <AddClientModal
        show={showAddClientModal}
        onClose={handleCloseAddModal}
        onAddClient={onAddClient}
        loading={loading}
        error={error}
        setError={setError}
      />

      <EditClientModal
        show={!!editingClient}
        client={editingClient}
        onClose={handleCloseEditModal}
        onUpdateClient={onUpdateClient}
        loading={loading}
        error={error}
        setError={setError}
      />
    </div>
  );
}

export default ClientSection;
