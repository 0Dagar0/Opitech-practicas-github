import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "../pages/Login/Login";
import Dashboard from "../pages/Dashboard/Dashboard";
import TicketList from "../pages/Tickets/TicketList";
import CreateTicket from "../pages/Tickets/CreateTicket";
import TicketDetail from "../pages/Tickets/TicketDetail";
import Reports from "../pages/Reports/Reports";
import Alerts from "../pages/Alerts/Alerts";

function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/tickets" element={<TicketList />} />
        <Route path="/tickets/new" element={<CreateTicket />} />
        <Route path="/tickets/detail" element={<TicketDetail />} />
        <Route path="/reports" element={<Reports />} />
        <Route path="/alerts" element={<Alerts />} />
      </Routes>
    </BrowserRouter>
  );
}

export default AppRoutes;