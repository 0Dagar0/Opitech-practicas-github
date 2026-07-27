import { useState } from "react";

export default function TicketDetail() {
  const [estado, setEstado] = useState("Abierto");
  const [comentario, setComentario] = useState("");
  const [comentarios, setComentarios] = useState([]);

  const enviarComentario = () => {
    if (comentario.trim() === "") return;

    setComentarios([...comentarios, comentario]);
    setComentario("");
  };

  return (
    <div style={{ padding: "30px" }}>
      <h1>Ticket #101</h1>

      <p>
        <strong>Técnico Asignado:</strong> Carlos Pérez
      </p>

      <p>
        <strong>Categoría:</strong> Hardware
      </p>

      <p>
        <strong>Prioridad:</strong> Alta
      </p>

      <p>
        <strong>Área:</strong> Contabilidad
      </p>

      <p>
        <strong>SLA:</strong> 2 Horas
      </p>

      <h2>Estado: {estado}</h2>

      <hr />

      <h3>Comentario</h3>

      <textarea
        value={comentario}
        onChange={(e) => setComentario(e.target.value)}
        placeholder="Escribir comentario"
        rows="4"
        style={{ width: "100%" }}
      />

      <br />
      <br />

      <button onClick={enviarComentario}>
        Enviar Comentario
      </button>

      <h3>Comentarios Registrados</h3>

      {comentarios.map((item, index) => (
        <p key={index}>• {item}</p>
      ))}

      <hr />

      <h3>Cambio de Estado</h3>

      <button onClick={() => setEstado("Asignado")}>
        Asignado
      </button>

      {" "}

      <button onClick={() => setEstado("En Proceso")}>
        En Proceso
      </button>

      {" "}

      <button onClick={() => setEstado("Resuelto")}>
        Resuelto
      </button>

      {" "}

      <button onClick={() => setEstado("Cerrado")}>
        Cerrado
      </button>

      {" "}

      <button onClick={() => setEstado("Reabierto")}>
        Reabierto
      </button>
    </div>
  );
}