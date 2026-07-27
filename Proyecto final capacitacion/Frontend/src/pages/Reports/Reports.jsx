import Sidebar from "../../components/Sidebar";
import "../Tickets/Tickets.css";

export default function Reports() {
  return (
    <div style={{ display: "flex" }}>
      <Sidebar />

      <div className="ticket-container">
        <h1 className="ticket-title">
          Reporte SLA
        </h1>

        <table className="ticket-table">
          <thead>
            <tr>
              <th>Técnico</th>
              <th>Resueltos</th>
              <th>Cumple</th>
              <th>No Cumple</th>
              <th>% SLA</th>
            </tr>
          </thead>

          <tbody>
            <tr>
              <td>Carlos Pérez</td>
              <td>10</td>
              <td>8</td>
              <td>2</td>
              <td>80%</td>
            </tr>

            <tr>
              <td>Ana Gómez</td>
              <td>12</td>
              <td>11</td>
              <td>1</td>
              <td>91%</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
}