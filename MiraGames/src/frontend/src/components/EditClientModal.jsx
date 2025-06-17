import { useState, useEffect } from "react";

function EditClientModal({
  show,
  client,
  onClose,
  onUpdateClient,
  loading,
  error,
  setError,
}) {
  const [editClientName, setEditClientName] = useState("");
  const [editClientEmail, setEditClientEmail] = useState("");
  const [editClientBalanceT, setEditClientBalanceT] = useState("");

  useEffect(() => {
    if (show && client) {
      setEditClientName(client.name);
      setEditClientEmail(client.email);
      setEditClientBalanceT(client.balanceT);
      setError("");
    }
  }, [show, client, setError]);

  const handleSubmit = (e) => {
    e.preventDefault();
    setError("");
    if (
      !client ||
      !editClientName ||
      !editClientEmail ||
      editClientBalanceT === ""
    ) {
      setError("Все поля клиента должны быть заполнены.");
      return;
    }
    onUpdateClient(
      client.id,
      editClientName,
      editClientEmail,
      parseFloat(editClientBalanceT)
    );
  };

  if (!show || !client) {
    return null;
  }

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h3>Редактировать клиента: {client.name}</h3>
        {error && <p className="error-message">{error}</p>}
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Имя:</label>
            <input
              type="text"
              value={editClientName}
              onChange={(e) => setEditClientName(e.target.value)}
              required
              className="form-control"
              disabled={loading}
            />
          </div>
          <div className="form-group">
            <label>Email:</label>
            <input
              type="email"
              value={editClientEmail}
              onChange={(e) => setEditClientEmail(e.target.value)}
              required
              className="form-control"
              disabled={loading}
            />
          </div>
          <div className="form-group">
            <label>Баланс:</label>
            <input
              type="number"
              step="0.01"
              value={editClientBalanceT}
              onChange={(e) => setEditClientBalanceT(e.target.value)}
              required
              className="form-control"
              disabled={loading}
            />
          </div>
          <div className="modal-actions">
            <button type="submit" className="btn" disabled={loading}>
              Сохранить
            </button>
            <button
              type="button"
              onClick={onClose}
              className="btn"
              style={{ backgroundColor: "#6c757d" }}
              disabled={loading}
            >
              Отмена
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default EditClientModal;
