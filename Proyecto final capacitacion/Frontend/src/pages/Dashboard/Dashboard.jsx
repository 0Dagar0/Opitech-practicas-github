import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Sidebar from "../../components/Sidebar";
import "./Dashboard.css";

export default function Dashboard() {
  const [ticketsAbiertos, setTicketsAbiertos] = useState(0);

  useEffect(() => {
    const tickets =
      JSON.parse(localStorage.getItem("tickets")) || [];

    setTicketsAbiertos(tickets.length);
  }, []);

  return (
    <div style={{ display: "flex" }}>
      <Sidebar />

      <div className="dashboard-container">
        <h1 className="dashboard-title">
          Dashboard OpiSupport
        </h1>

        <div className="cards-container">
          <div className="card">
            <h3>🎫 Tickets Abiertos</h3>
            <p>{ticketsAbiertos}</p>
          </div>

          <div className="card">
            <h3>👨‍💻 Técnicos Activos</h3>
            <p>5</p>
          </div>

          <div className="card">
            <h3>📊 Cumplimiento SLA</h3>
            <p>87%</p>
          </div>
        </div>

        <Link className="ticket-button" to="/tickets">
          Ver Tickets
        </Link>
      </div>
    </div>
  );
}