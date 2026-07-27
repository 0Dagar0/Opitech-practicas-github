import Sidebar from "../../components/Sidebar";
import "../Tickets/Tickets.css";

export default function Alerts() {
  return (
    <div style={{ display: "flex" }}>
      <Sidebar />

      <div className="ticket-container">
        <h1 className="ticket-title">
          Alertas
        </h1>

        <div className="form-card">
          <p>🔴 Ticket 104 vencido</p>

          <p>🟡 Ticket 108 vence en 45 minutos</p>

          <p>🔴 SLA incumplido en ticket 112</p>
        </div>
      </div>
    </div>
  );
}