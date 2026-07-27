import { Link } from "react-router-dom";
import "./Sidebar.css";

export default function Sidebar() {
  return (
    <div className="sidebar">
      <h2 className="sidebar-logo">
        🚀 OpiSupport
      </h2>

      <Link className="menu-item" to="/dashboard">
        🏠 Dashboard
      </Link>

      <Link className="menu-item" to="/tickets">
        🎫 Tickets
      </Link>

      <Link className="menu-item" to="/tickets/new">
        ➕ Nuevo Ticket
      </Link>

      <Link className="menu-item" to="/reports">
        📊 Reportes
      </Link>

      <Link className="menu-item" to="/alerts">
        🔔 Alertas
      </Link>
    </div>
  );
}