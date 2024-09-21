<template>
    <div class="container">
        <button @click="cambiarEstadoForm" class="btn-float">+</button>


        <!-- Formulario para agregar nueva tarea -->
        <!-- El formulario usa el v-model para enlazar las propiedades nombre, 
          descripcion y completada del objeto nuevaTarea 

        Al enviar el formulario, se llama al método agregarTarea.
        -->

        <!-- <div v-if="isFormVisible" class="form-container bg-gray-300"> -->
        <div :class="['tareas-form', { 'visible': isFormVisible }, 'bg-gray-300']">
            <h2>{{ editando ? 'Editar tarea' : 'Agregar nueva tarea' }}</h2>
            <form @submit.prevent="agregarTarea">
                <div>
                    <label for="nombre">Nombre tarea</label>
                    <input type="text" v-model="nuevaTarea.nombre" class="mt-1 block w-full p-2 border rounded"
                        required>
                </div>
                <div>
                    <label for="descripcion">Descripción: </label>
                    <input type="text" v-model="nuevaTarea.descripcion" class="mt-1 block w-full p-2 border rounded"
                        required>
                </div>
                <div>
                    <label for="completada">Completada:</label>
                    <input type="checkbox" v-model="nuevaTarea.completada">
                </div>
                <button type="submit" class="w-full bg-gray-600 text-white py-2 rounded hover:bg-gray-700">
                    {{ editando ? 'Guardar Cambios' : 'Agregar Tarea' }}
                </button>
            </form>
        </div>

        <!-- Listado de Tareas -->
        <div :class="['lista-tareas', { 'expanded': isFormVisible && !isMovile }]">
            <h2>Lista de tareas</h2>

            <div class="grid block w-full gap-4 p-4">
                <div v-for="tarea in tareas" :key="tarea.id" class="bg-white shadow-md rounded-lg p-4">
                    <h3 class="text-lg font-bold">{{ tarea.nombre }}</h3>
                    <p class="text-sm text-gray-600">{{ tarea.descripcion }}</p>
                    <div class="mt-4 flex justify-between items-center">
                        <!-- agregar el cher de completada aqui -->
                        <div class="space-x-2">
                            <button @click="editarTarea(tarea.id)"
                                class="bg-blue-500 text-white px-4 py-2 rounded">Editar</button>
                            <!-- <router-link :to="`/tareas/${tarea.id}/editar`">Editar</router-link> -->
                            <button @click="eliminarTarea(tarea.id)"
                                class="bg-red-500 text-white px-4 py-2 rounded">Eliminar</button>

                        </div>
                    </div>
                </div>


            </div>


        </div>
    </div>
</template>

<script>
import axios from "axios";

const URL = "http://localhost:5180/api/tareas";

export default {
    data() {
        return {
            tareas: [], // Lista de tareas que se llenará desde el backend
            isFormVisible: false,
            isMobile: false,
            nuevaTarea: {
                nombre: '',
                descripcion: '',
                completada: false
            },
            editando: false, // Indica si estamos en modo edición
            tareaSeleccionada: null // La tarea que estamos editando
        };
    },

    mounted() {
        // Detectar si es dispositivo móvil
        this.checkIfMobile();
        window.addEventListener('resize', this.checkIfMobile);

        // Llama al método obtenerTareas cuando el componente se monta
        this.obtenerTareas();
    },

    beforeUnmount() {
        window.removeEventListener('resize', this.checkIfMobile);
    },

    methods: {
        // Método para obtener las tareas
        async obtenerTareas() {
            try {
                // Obtener el token desde localStorage
                const token = localStorage.getItem("token");

                // Realizar una solicitud GET con el token en el encabezado Authorization
                const response = await axios.get(URL, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });
                console.log(response.data); // Procesar las tareas recibidas
                this.tareas = response.data; // Asigna las tareas recibidas a la variable 'tareas'

            } catch (error) {
                console.error("Error al obtener tareas", error);
            }
        },

        // Método para agregar una nueva tarea
        async agregarTarea() {
            try {

                /* agregarTarea, usamos Axios para enviar la nueva tarea al backend. Nos aseguramos de pasar el 
                token JWT en las cabeceras de la solicitud para que el backend autorice la operación. */
                const TOKEN = localStorage.getItem('token');

                if (this.editando) {
                    // Actualizar tarea
                    const response = await axios.put(URL + `/${this.tareaSeleccionada.id}`, this.nuevaTarea, {
                        headers: {
                            Authorization: `Bearer ${TOKEN}`
                        }
                    });

                    // Actualizar la lista localmente
                    const index = this.tareas.findIndex(t => t.id === this.tareaSeleccionada.id);
                    if (index !== -1) {
                        console.log("Datos recibidos del servidor para actualizar tarea:", response.data);
                        this.tareas[index] = response.data;
                    }
                    alert('Tarea actualizada con éxito');

                } else {
                    // Agregar nueva tarea
                    const response = await axios.post(URL, this.nuevaTarea, {
                        headers: {
                            Authorization: `Bearer ${TOKEN}`
                        }
                    });
                    /* Una vez que la tarea se agrega con éxito, la incluimos en el array tareas y reiniciamos el formulario. */
                    this.tareas.push(response.data);
                    alert('Tarea agregada con éxito');

                }

                // Reiniciar formulario
                this.nuevaTarea = {
                    nombre: '',
                    descripcion: '',
                    completada: false
                };

                this.isFormVisible = true;
                this.editando = false;

            } catch (error) {
                console.error('Error al agregar/editar tarea:', error);
                alert('Error al agregar/editar tarea');
            }
        },

        // Método para eliminar una tarea
        async eliminarTarea(id) {
            try {
                const TOKEN = localStorage.getItem('token');
                await axios.delete(URL + `/${id}`, {
                    headers: {
                        Authorization: `Bearer ${TOKEN}`
                    }
                });
                this.tareas = this.tareas.filter(tarea => tarea.id !== id);
                alert('Tarea eliminada con éxito');

            } catch (error) {
                console.error('Error al eliminar tarea:', error);
            }
        },

        // editar las tarea
        async editarTarea(id) {
            // Buscar la tarea seleccionada
            const tarea = this.tareas.find(t => t.id === id);
            if (tarea) {
                // Rellenar el formulario con los valores de la tarea seleccionada
                this.nuevaTarea = { ...tarea };
                this.tareaSeleccionada = tarea;
                this.isFormVisible = true;
                this.editando = true;
            }
        },

        async cambiarEstadoForm() {
            this.isFormVisible = !this.isFormVisible;
            if (!this.isFormVisible) {
                // Resetear si se cierra el formulario
                this.nuevaTarea = { nombre: '', descripcion: '', completada: false };
                this.editando = false;
            }

        },

        checkIfMobile() {
            this.isMobile = window.innerWidth <= 768;
        }
    },
};
</script>

<style scoped>
/* Contenedor Principal */
.container {
    display: flex;
    flex-wrap: wrap;
}

/* Listado de Tareas */
.lista-tareas {
    flex: 1;
    transition: margin-left 0.3s ease;
    /* Añade un margen dinámico cuando el formulario se expanda */
}


/* Cuando el formulario está visible en pantallas grandes */
.lista-tareas.expanded {
    margin-left: 35%;
    /* Contraer la lista para dejar espacio al formulario (40% ancho del form) */

}

/* Formulario de Crear Tarea */
.tareas-form {
    display: none;
    transition: transform 0.3s ease, opacity 0.3s ease;
    /* opacity: 0; */
}



/* Estilo del formulario en pantallas grandes */
@media (min-width: 769px) {
    .tareas-form {
        position: fixed;
        top: 0;
        left: 0;
        height: 100%;
        width: 32%;
        /* Ancho del formulario en pantallas grandes */
        background-color: #ffffff;
        padding: 20px;
        box-shadow: 2px 0 5px rgba(0, 0, 0, 0.2);
        /* Sombra para darle efecto de barra lateral */
        z-index: 100;
        transform: translateX(-100%);
        /* Desplaza el formulario fuera de la vista hacia la izquierda */
        opacity: 0;
    }

    .tareas-form.visible {
        display: block;
        transform: translateX(0);
        /* Cuando es visible, regresa a su posición */
        opacity: 1;
    }
}

/* Estilo del formulario en dispositivos móviles */
@media (max-width: 768px) {
    .tareas-form {
        position: relative;
        width: 100%;
        margin-top: 10px;
        height: auto;
        transform: none;
        opacity: 1;
    }

    .lista-tareas.expanded {
        margin-right: 0;
        /* En móviles no se contrae el listado de tareas */
    }
}

/* Botón Flotante */
.btn-float {
    position: fixed;
    bottom: 20px;
    right: 500px;
    background-color: #007bff;
    color: white;
    border-radius: 50%;
    width: 60px;
    height: 60px;
    font-size: 24px;
    border: none;
    cursor: pointer;
}

h2 {
    font-size: 30px;
    color: rgba(0, 0, 0, 0.884);
    font-style: italic;
}
</style>