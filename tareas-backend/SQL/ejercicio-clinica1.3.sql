-- insertar medicos
INSERT INTO medicos (nombre_completo, especialidad, numero_licencia) VALUES
  ('Dra. Camila Ríos',    'Cardiología',  'LIC-004'),
  ('Dr. Andrés Mora',     'Pediatría',    'LIC-005'),
  ('Dra. Sofía Vargas',   'Dermatología', 'LIC-006');

-- insertar pacientes
INSERT INTO pacientes (nombre, fecha_nacimiento, telefono, numero_documento) VALUES
  ('Juan Pérez',    '1985-03-12', '3001234567', 'DOC-004'),
  ('Laura Gómez',   '1992-07-25', '3109876543', 'DOC-005'),
  ('Carlos Díaz',   '1978-11-03', '3205551234', 'DOC-006'),
  ('María Soto',    '2001-01-18', '3006667788', 'DOC-007');

-- insertar citas
INSERT INTO citas (id_paciente, id_medico, fecha_hora, motivo_consulta, estado) VALUES
  (4, 1, '2025-08-01 09:00', 'Control tensión',   'pendiente'),
  (5, 1, '2025-08-01 10:00', 'Revisión anual',    'completada'),
  (6, 2, '2025-08-02 08:30', 'Fiebre recurrente', 'pendiente'),
  (7, 2, '2025-08-02 09:30', 'Vacunación',        'completada'),
  (4, 3, '2025-08-03 11:00', 'Dermatitis',        'cancelada'),
  (6, 3, '2025-08-04 14:00', 'Seguimiento',       'pendiente');

SELECT id_paciente, nombre FROM pacientes ORDER BY id_paciente;

SELECT  COUNT (*) FROM  pacientes;


SELECT m.nombre_completo, c.fecha_hora, c.estado 
FROM citas c
RIGHT JOIN medicos m ON c.id_medico = m.id_medico
ORDER BY m.nombre_completo;


SELECT COUNT(*) AS total_medicos FROM medicos;
SELECT COUNT(DISTINCT id_medico) AS medicos_con_citas FROM citas;

SELECT m.nombre_completo, COUNT(c.id_cita) AS total_citas
FROM medicos m
LEFT JOIN citas c ON m.id_medico = c.id_medico
GROUP BY m.nombre_completo, m.id_medico
ORDER BY m.nombre_completo;

SELECT m.especialidad, COUNT(c.id_cita) AS total_citas
FROM medicos m
LEFT JOIN citas c ON m.id_medico = c.id_medico
GROUP BY m.especialidad
ORDER BY m.especialidad;

--  mostrar las primeras 3 citas ordenadas por fecha.
SELECT c.id_cita, m.nombre_completo AS medico, p.nombre AS paciente, c.fecha_hora
FROM citas c
JOIN medicos m ON c.id_medico = m.id_medico
JOIN pacientes p ON c.id_paciente = p.id_paciente
ORDER BY c.fecha_hora ASC
LIMIT 3 OFFSET 0;

--Mostrar la página 2 de citas (saltar las primeras 3, mostrar las siguientes 3).
SELECT c.id_cita, m.nombre_completo AS medico, p.nombre AS paciente, c.fecha_hora
FROM citas c
JOIN medicos m ON c.id_medico = m.id_medico
JOIN pacientes p ON c.id_paciente = p.id_paciente
ORDER BY c.fecha_hora ASC
LIMIT 3 OFFSET 3;
