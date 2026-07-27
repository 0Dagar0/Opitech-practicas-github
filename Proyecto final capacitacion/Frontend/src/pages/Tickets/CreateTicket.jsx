import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Sidebar from "../../components/Sidebar";
import "./Tickets.css";

export default function CreateTicket() {
  const navigate = useNavigate();

  const [titulo, setTitulo] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [categoria, setCategoria] = useState("Hardware");
  const [prioridad, setPrioridad] = useState("Media");

  const handleSubmit = (e) => {
    e.preventDefault();

    const tickets =
      JSON.parse(localStorage.getItem("tickets")) || [];

    const nuevoTicket = {
      id: Date.now(),
      titulo,
      descripcion,
      categoria,
      prioridad,
      estado: "Abierto",
      fecha: new Date().toLocaleDateString(),
    };

    tickets.push(nuevoTicket);

    localStorage.setItem(
      "tickets",
      JSON.stringify(tickets)
    );

    navigate("/tickets");
  };

  return (
    <div style={{ display: "flex" }}>
      <Sidebar />

      <div className="ticket-container">
        <div className="form-card">
          <h1 className="ticket-title">
            Nuevo Ticket
          </h1>

          <form onSubmit={handleSubmit}>
            <input
              type="text"
              placeholder="Título"
              value={titulo}
              onChange={(e) =>
                setTitulo(e.target.value)
              }
              required
            />

            <textarea
              rows="5"
              placeholder="Descripción"
              value={descripcion}
              onChange={(e) =>
                setDescripcion(e.target.value)
              }
              required
            ></textarea>

            <div className="selects-container">
              <select
                value={categoria}
                onChange={(e) =>
                  setCategoria(e.target.value)
                }
              >
                <option>Hardware</option>
                <option>Software</option>
                <option>Red</option>
                <option>Otro</option>
              </select>

              <select
                value={prioridad}
                onChange={(e) =>
                  setPrioridad(e.target.value)
                }
              >
                <option>Baja</option>
                <option>Media</option>
                <option>Alta</option>
                <option>Crítica</option>
              </select>
            </div>

            <button type="submit">
              Crear Ticket
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}