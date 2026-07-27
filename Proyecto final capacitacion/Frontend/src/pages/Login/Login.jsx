import React, { useState } from "react";
import "./Login.css";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const navigate = useNavigate();

  const [usuario, setUsuario] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = (e) => {
    e.preventDefault();

    if (
      usuario === "supervisor@opisupport.com" &&
      password === "123456"
    ) {
      localStorage.setItem("rol", "supervisor");
      localStorage.setItem("usuario", usuario);

      navigate("/dashboard");
    } 
    
    else if (
      usuario === "tecnico@opisupport.com" &&
      password === "123456"
    ) {
      localStorage.setItem("rol", "tecnico");
      localStorage.setItem("usuario", usuario);

      navigate("/dashboard");
    } 
    
    else {
      alert("Usuario o contraseña incorrectos");
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        <h1>OpiSupport</h1>

        <p className="subtitle">
          Sistema de Gestión de Tickets
        </p>

        <form onSubmit={handleLogin}>
          <label>Usuario</label>

          <input
            type="text"
            placeholder="Ingrese su usuario"
            value={usuario}
            onChange={(e) => setUsuario(e.target.value)}
          />

          <label>Contraseña</label>

          <input
            type="password"
            placeholder="Ingrese su contraseña"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <button type="submit">
            Iniciar Sesión
          </button>

          <p className="forgot-password">
            ¿Olvidaste tu contraseña?
          </p>
        </form>
      </div>
    </div>
  );
}