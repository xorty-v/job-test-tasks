// src/components/PaymentsSection.jsx
import React from "react";

function PaymentsSection({ payments, loading, error }) {
  return (
    <div className="container-card">
      <h3>История платежей</h3>
      {loading ? (
        <p>Загрузка платежей...</p>
      ) : error ? (
        <p className="error-message">{error}</p>
      ) : payments.length > 0 ? (
        <table className="data-table" style={{ marginTop: "0" }}>
          <thead>
            <tr>
              <th>Email Клиента</th>
              <th>Сумма</th>
              <th>Дата</th>
            </tr>
          </thead>
          <tbody>
            {payments.map((payment, index) => (
              <tr key={index}>
                <td>{payment.clientEmail}</td>
                <td>{payment.amount.toFixed(2)}</td>
                <td>{new Date(payment.timestampOnUtc).toLocaleString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>Платежи не найдены.</p>
      )}
    </div>
  );
}

export default PaymentsSection;
