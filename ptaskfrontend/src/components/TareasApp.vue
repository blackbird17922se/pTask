<template>
    <div>
        <h1>Lista de tareas</h1>
        <ul>
            <li v-for="tarea in tareas" :key="tarea.id">
                {{ tarea.nombre }} - {{ tarea.descripcion }} - Completada: {{ tarea.completada }}
                <!-- btn eliminar -->
                 <button @click="eliminarTarea(tarea.id)">Eliminar</button>
                 <router-link :to="`/tareas/${tarea.id}/editar`">Editar</router-link>
            </li>
        </ul>

        <!-- Formulario para agregar nueva tarea -->
         <!-- El formulario usa el v-model para enlazar las propiedades nombre, 
          descripcion y completada del objeto nuevaTarea 

        Al enviar el formulario, se llama al método agregarTarea.
        -->
         <h2>Agregar nueva tarea</h2>
         <form @submit.prevent="agregarTarea">
            <div>
                <label for="nombre">Nombre tarea</label>
                <input type="text" v-model="nuevaTarea.nombre" required>
            </div>
            <div>
                <label for="descripcion">Descripción: </label>
                <input type="text" v-model="nuevaTarea.descripcion" required>
            </div>
            <div>
                <label for="completada">Completada:</label>
                <input type="checkbox" v-model="nuevaTarea.completada">
            </div>
            <button type="submit">Agregar Tarea</button>
         </form>
    </div>
</template>

<script>
import axios from "axios";

const URL = "http://localhost:5180/api/tareas";

export default {
    data(){
        return{
            tareas: [], // Lista de tareas que se llenará desde el backend
            nuevaTarea:{
                nombre:'',
                descripcion:'',
                completada: false
            }
        };
    },

    methods: {
        // Método para obtener las tareas
        async obtenerTareas(){
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
        async agregarTarea(){
            try {

                /* agregarTarea, usamos Axios para enviar la nueva tarea al backend. Nos aseguramos de pasar el 
                token JWT en las cabeceras de la solicitud para que el backend autorice la operación. */
                const TOKEN = localStorage.getItem('token');
                const response = await axios.post(URL, this.nuevaTarea, {
                    headers: {
                        Authorization: `Bearer ${TOKEN}`
                    }
                });
                /* Una vez que la tarea se agrega con éxito, la incluimos en el array tareas y reiniciamos el formulario. */
                this.tareas.push(response.data);

                // Reiniciar formulario
                this.nuevaTarea = {
                    nombre:'',
                    descripcion:'',
                    completada: false
                };
                alert('Tarea agregada con éxito');

            } catch (error) {
                console.error('Error al agregar tarea:', error);
                alert('Error al agregar tarea');      
            }
        },

        // Método para eliminar una tarea
        async eliminarTarea(id){
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
        }

        // editar las tarea
        // async editarTarea(id){
        //     try {
        //         const TOKEN = localStorage.getItem('token');
                
        //     } catch (error) {
                
        //     }
        // }
    },


    mounted() {
        // Llama al método obtenerTareas cuando el componente se monta
        this.obtenerTareas();
    },
   
};
</script>

<style scoped>
/* Agrega tus estilos aquí */
</style>