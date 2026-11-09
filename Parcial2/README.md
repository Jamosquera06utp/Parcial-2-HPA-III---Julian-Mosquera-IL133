# LabMVP — Laboratorio de MVP (Parcial Práctico 2)

Aplicación de escritorio en **C# / Windows Forms** que implementa el patrón **MVP** en capas, con login seguro (hash de contraseñas), formulario **MDI** con menú lateral, reloj, sesión del usuario y el **Juego de Craps**.

## Datos del autor

| Campo | Detalle |
|---|---|
| Autor | _Nombre y apellido_ |
| Grupo | 1IL133 |
| Curso | Herramientas de la Programación Aplicada III (.Net) |
| Instructora | Ing. Irina Fong |
| Universidad | Universidad Tecnológica de Panamá — Facultad de Ingeniería en Sistemas, Campus Víctor Levis Sasso |
| Fecha | _dd de mes de 2026_ |

## Tecnologías y versiones

| Tecnología | Versión |
|---|---|
| .NET | 8.0 (net8.0-windows) |
| Lenguaje | C# 12 |
| Interfaz | Windows Forms |
| Base de datos | MySQL 8.x |
| Conector | MySql.Data 9.1.0 (NuGet) |
| IDE | Visual Studio 2022 |
| Hash | PBKDF2-SHA256 con salt aleatorio (`System.Security.Cryptography`) |

## Arquitectura (MVP en capas)

```
LabMVP/
├── Entidades/       Usuario, SesionUsuario
├── Datos/           ConexionBD, IUsuarioRepositorio, UsuarioRepositorio (Stored Procedures)
├── Negocio/         IHashService, IVerificarHashService, Pbkdf2PasswordService,
│                    IAutenticacionService, AutenticacionService, Validador, JuegoCrapsService
├── Presentadores/   I...View + ...Presenter (Login, Registro, Principal, Craps)
├── Vistas/          FrmLogin, FrmRegistro, FrmPrincipal (MDI), FrmInicio, FrmCraps
└── BaseDatos/       script.sql
```

- **Dos contratos independientes** en Negocio: `IHashService` (genera el hash) e `IVerificarHashService` (verifica texto plano vs. hash). `Pbkdf2PasswordService` implementa ambos.
- **Conexión específica** con el usuario `labmvp_app`, que solo tiene `EXECUTE` sobre la base de datos (mínimos privilegios).
- Las **vistas** no conocen el hash ni la base de datos; solo hablan con su **presentador**.

## Proceso de instalación

1. Clonar el repositorio: `git clone <URL-del-repositorio>`
2. Abrir MySQL y ejecutar `BaseDatos/script.sql` (crea la BD, tabla, procedimientos y el usuariao `labmvp_app`).
3. (Opcional) Cambiar la clave de `labmvp_app` en el script y en `Datos/ConexionBD.cs`.
4. Abrir `LabMVP.csproj` en Visual Studio 2022 (restaura MySql.Data automáticamente).
5. Ejecutar con **F5**. Crear un usuario desde *“¿No tienes cuenta? Regístrate”* e iniciar sesión.

## Controles utilizados

`Form` (MDI container), `Panel`, `Button`, `Label`, `TextBox` (con `UseSystemPasswordChar`), `LinkLabel`, `Timer`, `MessageBox`.

## Capturas

### Resultado del MDI
![MDI 1](docs/mdi-1.png)
![MDI 2](docs/mdi-2.png)

### Resultado del Juego de Craps
![Craps 1](docs/craps-1.png)
![Craps 2](docs/craps-2.png)

### Resultado de las Validaciones
![Validación 1](docs/validacion-1.png)
![Validación 2](docs/validacion-2.png)
