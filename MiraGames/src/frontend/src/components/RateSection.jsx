import React, { useState } from "react";

function RateSection({ currentRate, onRateChange, loading, error, setError }) {
  const [newRateValue, setNewRateValue] = useState(currentRate || "");

  React.useEffect(() => {
    if (currentRate !== null) {
      setNewRateValue(currentRate);
    }
  }, [currentRate]);

  const handleSubmit = (e) => {
    e.preventDefault();
    setError("");
    if (newRateValue === "") {
      setError("Пожалуйста, введите значение курса.");
      return;
    }
    onRateChange(parseFloat(newRateValue));
  };

  return (
    <div className="container-card">
      <h3>
        Курс токенов:{" "}
        {loading
          ? "Загрузка..."
          : currentRate !== null
          ? currentRate.toFixed(2)
          : "N/A"}
      </h3>
      {error && <p className="error-message">{error}</p>}
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <input
            type="number"
            step="0.01"
            value={newRateValue}
            onChange={(e) => setNewRateValue(e.target.value)}
            className="form-control"
            placeholder="Новый курс"
            required
            disabled={loading}
          />
        </div>
        <button type="submit" className="btn" disabled={loading}>
          Изменить курс
        </button>
      </form>
    </div>
  );
}

export default RateSection;
