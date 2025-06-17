import React, { useState, useEffect } from "react";

function AddClientModal({
  show,
  onClose,
  onAddClient,
  loading,
  error,
  setError,
}) {
  const [newClientName, setNewClientName] = useState("");
  const [newClientEmail, setNewClientEmail] = useState("");

  useEffect(() => {
    if (show) {
      setNewClientName("");
      setNewClientEmail("");
      setError("");
    }
  }, [show, setError]);

  const handleSubmit = (e) => {
    e.preventDefault();
    setError("");
    if (!newClientName || !newClientEmail) {
      setError("Имя и Email клиента не могут быть пустыми.");
      return;
    }
    onAddClient(newClientName, newClientEmail);
  };

  if (!show) {
    return null;
  }

  return (
    <div className="modal-overlay add-client-modal">
      <div className="modal-content">
        <h3>Добавить нового клиента</h3>
        {error && <p className="error-message">{error}</p>}
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label>Имя:</label>
            <input
              type="text"
              value={newClientName}
              onChange={(e) => setNewClientName(e.target.value)}
              required
              className="form-control"
              disabled={loading}
            />
          </div>
          <div className="form-group">
            <label>Email:</label>
            <input
              type="email"
              value={newClientEmail}
              onChange={(e) => setNewClientEmail(e.target.value)}
              required
              className="form-control"
              disabled={loading}
            />
          </div>
          <div className="modal-actions">
            <button type="submit" className="btn" disabled={loading}>
              Добавить
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

export default AddClientModal;
