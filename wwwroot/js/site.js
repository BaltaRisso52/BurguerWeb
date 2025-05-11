// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// logica para agregar al carrito

document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.btn-agregar-carrito').forEach(button => {
        button.addEventListener('click', function () {
            const productoId = parseInt(this.getAttribute('data-id'));
            const select = this.closest('.producto').querySelector('.select-variante');
            const varianteId = select?.value ? parseInt(select.value) : null;

            fetch('/Carrito/Agregar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ productoId, varianteId })
            })
                .then(res => res.json())
                .then(data => {
                    console.log("Producto agregado al carrito:", data);
                    if (data.ok) {
                        alert("¡Producto agregado al carrito!");
                    } else {
                        alert("Hubo un problema al agregar al carrito.");
                    }
                })
                .catch(err => console.error("Error al agregar al carrito:", err));
        });
    });
});