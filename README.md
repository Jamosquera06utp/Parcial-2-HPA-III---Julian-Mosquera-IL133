# Parcial Práctico 2

**Fecha:** 09/11/2026

---

## 📚 Contenido del Repositorio

En este laboratorio se desarrolló una aplicación de escritorio utilizando C# y Windows Forms en Visual Studio, aplicando el patrón de diseño **MVP (Modelo - Vista - Presentador)** organizado en capas, con inicio de sesión seguro mediante hash de contraseñas, contenedor MDI con menú lateral y lógica de juego.

El repositorio está compuesto por un único proyecto (`LabMVP`) que incluye:

* 🔐 **Login y Registro** - Validaciones y hash de contraseñas
* 🗂️ **Interfaz MDI** - Menú lateral, reloj y sesión del usuario
* 🎲 **Juego de Craps** - Lógica del juego dentro del patrón MVP

---

## 🛠️ Tecnologías Utilizadas

* 💻 **Lenguaje:** C# 12
* 🧩 **Framework:** .NET 8.0 (net8.0-windows)
* 🖥️ **IDE:** Visual Studio 2022
* 🪟 **Interfaz:** Windows Forms
* 🗄️ **Base de datos:** MySQL 8.x (conector `MySql.Data` 9.1.0)
* 🔒 **Seguridad:** PBKDF2-SHA256 con salt aleatorio (`System.Security.Cryptography`)
* 📦 **Control de versiones:** Git & GitHub

---

## ⚙️ Proceso de Instalación

1. Clonar el repositorio: `git clone <URL-del-repositorio>`
2. Abrir MySQL y ejecutar el script `BaseDatos/script.sql` (crea la base de datos `labmvp_db`, la tabla, los procedimientos almacenados y el usuario `labmvp_app` con privilegios mínimos).
3. (Opcional) Cambiar la clave de `labmvp_app` en el script y en `Datos/ConexionBD.cs`.
4. Abrir `LabMVP.csproj` en Visual Studio 2022 (restaura `MySql.Data` automáticamente).
5. Ejecutar con **F5**, registrar un usuario desde *"¿No tienes cuenta? Regístrate"* e iniciar sesión.

---

## 🧱 Arquitectura del Proyecto

* **Entidades:** clases que representan los datos (`Usuario`, `SesionUsuario`).
* **Datos:** conexión específica a MySQL y repositorio con Stored Procedures.
* **Negocio:** dos contratos independientes — `IHashService` (genera el hash) e `IVerificarHashService` (verifica el hash) — implementados por `Pbkdf2PasswordService`, además de la autenticación, validaciones y lógica de Craps.
* **Presentadores:** intermediarios entre la Vista y el Negocio.
* **Vistas:** formularios que solo muestran controles y avisan al Presentador.

---

## 🧰 Controles Utilizados

* `Form` (con `IsMdiContainer`), `Panel`, `Button`, `Label`, `TextBox` (con `UseSystemPasswordChar`), `LinkLabel`, `Timer`, `MessageBox`.

---

## 📸 Capturas de Pantalla y Problemas

### 🗂️ Problema 1: Interfaz MDI con menú lateral 

En este ejercicio se desarrolló una interfaz de múltiples documentos (MDI) con un menú lateral colapsable, la hora y fecha actuales, y en la parte inferior del menú la sesión del usuario que inició sesión.

#### 📸 Captura de pantalla

<img width="366" height="489" alt="image" src="https://github.com/user-attachments/assets/c44e36d2-a0e8-4bd7-a91a-1ad6525b9c20" />

<img width="1078" height="655" alt="image" src="https://github.com/user-attachments/assets/785cbd93-68b5-44e1-ac51-360a773a591d" />


---

### 🎲 Problema 2: Juego de Craps 

En este ejercicio se programó la lógica del clásico juego de dados de Craps dentro del patrón MVP. El usuario gana con 7 u 11 en el primer tiro, pierde con 2, 3 o 12, y con cualquier otro número establece su punto y sigue lanzando hasta repetirlo (gana) o sacar 7 (pierde).

#### 📸 Captura de pantalla

<img width="1073" height="668" alt="image" src="https://github.com/user-attachments/assets/8ea6e3ea-4826-4748-9d23-85857617a4a3" />
<img width="1071" height="653" alt="image" src="https://github.com/user-attachments/assets/8ae9e57d-6368-4eef-b1c0-85531d454277" />


---

### ✅ Problema 3: Validaciones y Login

En este ejercicio se implementaron validaciones de correo, contraseña (mínimo 8 caracteres, una mayúscula y un número), confirmación de contraseña y correo duplicado, junto con la generación y verificación del hash.

#### 📸 Captura de pantalla

<img width="370" height="516" alt="image" src="https://github.com/user-attachments/assets/0b0ac5c5-671c-4e8a-a426-7047018774fa" />

<img width="370" height="520" alt="image" src="https://github.com/user-attachments/assets/bbb87641-15db-4c41-8676-92ba8a2db288" />

---

## 📁 Estructura de Carpetas o Directorios

```text
LabMVP/
│
├── Entidades/
│   ├── Usuario.cs
│   └── SesionUsuario.cs
│
├── Datos/
│   ├── ConexionBD.cs
│   ├── IUsuarioRepositorio.cs
│   └── UsuarioRepositorio.cs
│
├── Negocio/
│   ├── IHashService.cs
│   ├── IVerificarHashService.cs
│   ├── Pbkdf2PasswordService.cs
│   ├── IAutenticacionService.cs
│   ├── AutenticacionService.cs
│   ├── Validador.cs
│   └── JuegoCrapsService.cs
│
├── Presentadores/
│   ├── ILoginView.cs / LoginPresenter.cs
│   ├── IRegistroView.cs / RegistroPresenter.cs
│   ├── IPrincipalView.cs / PrincipalPresenter.cs
│   └── ICrapsView.cs / CrapsPresenter.cs
│
├── Vistas/
│   ├── FrmLogin.cs
│   ├── FrmRegistro.cs
│   ├── FrmPrincipal.cs (Contenedor MDI Principal)
│   ├── FrmInicio.cs
│   ├── FrmCraps.cs
│   └── EstiloUI.cs
│
├── BaseDatos/
│   └── script.sql
│
├── docs/ (capturas)
├── Program.cs
├── LabMVP.csproj
└── README.md
```

---

## 👨‍💻 Autor y Contexto

* **Nombre:** Julian Mosquera
* **Institución:** Universidad Tecnológica de Panamá (UTP)
* **Curso:** Herramientas de la Programación Aplicada III — Grupo 1IL133
* **Instructora:** Ing. Irina Fong
* **Fecha de Realización:** 09/11/2026
