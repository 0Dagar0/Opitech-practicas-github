INSERT INTO medicos (nombre_completo, especialidad, numero_licencia) VALUES
('Dr. Juan Pérez', 'Cardiología', 'LIC-001'),
('Dra. María Gómez', 'Pediatría', 'LIC-002'),
('Dr. Carlos López', 'Dermatología', 'LIC-003');


INSERT INTO pacientes (nombre, fecha_nacimiento, telefono, numero_documento) VALUES
('Ana Rodríguez', '1990-05-15', '3001234567', 'DOC-001'),
('Luis Martínez', '1985-08-22', '3007654321', 'DOC-002'),
('Sofía Ramírez', '2000-03-10', '3009876543', 'DOC-003');


INSERT INTO citas (id_paciente, id_medico, fecha_hora, motivo_consulta, estado) VALUES
(1, 1, '2026-05-10 09:00:00', 'Dolor en el pecho', 'pendiente'),
(2, 2, '2026-05-10 10:30:00', 'Control pediátrico', 'completada'),
(3, 3, '2026-05-11 11:00:00', 'Erupción en la piel', 'pendiente'),
(1, 2, '2026-05-12 08:30:00', 'Fiebre y tos', 'cancelada');

select * from medicos;

select * from pacientes;

SELECT 
    c.id_cita,
    p.nombre AS paciente,
    m.nombre_completo AS medico,
    c.fecha_hora,
    c.motivo_consulta,
    c.estado
FROM citas c
JOIN pacientes p ON c.id_paciente = p.id_paciente
JOIN medicos m ON c.id_medico = m.id_medico;
