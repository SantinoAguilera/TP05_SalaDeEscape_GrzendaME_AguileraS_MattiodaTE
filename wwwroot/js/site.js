// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function abrirCerrarPistas() {
  var pistasDiv = document.getElementById("pistas");
  pistasDiv.style.display = pistasDiv.style.display === "flex"
    ? "none"
    : "flex";
}
