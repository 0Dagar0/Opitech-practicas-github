import { Link } from "react-router-dom";
import Sidebar from "../../components/Sidebar";
import "./Tickets.css";

export default function TicketList() {
  const tickets =
    JSON.parse(localStorage.getItem("tickets")) || [];

  return (
    <div style={{ display: "flex" }}>
      <Sidebar />

      <div className="ticket-container">
        <h1 className="ticket-title">
          Tickets
        </h1>

        <Link
          className="new-ticket-btn"
          to="/tickets/new"
        >
          Nuevo Ticket
        </Link>

        <table className="ticket-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Título</th>
              <th>Estado</th>
              <th>Prioridad</th>
              <th>Acciones</th>
            </tr>
          </thead>

          <tbody>
            {tickets.map((ticket) => (
              <tr key={ticket.id}>
                <td>{ticket.id}</td>
                <td>{ticket.titulo}</td>
                <td>{ticket.estado}</td>
                <td>{ticket.prioridad}</td>
                <td>
                  <Link
                    className="new-ticket-btn"
                    to="/tickets/detail"
                  >
                    Ver Detalle
                  </Link>
                </td>
              </tr>
            ))}

            {tickets.length === 0 && (
              <tr>
                <td colSpan="5">
                  No hay tickets registrados
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}