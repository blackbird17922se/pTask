<template>
    <div>
        <h1>Editar tarea</h1>

         <form @submit.prevent="editarTarea">
            <div>
                <label for="nombre">Nombre tarea</label>
                <input type="text" v-model="tarea.nombre" id="nombre">
            </div>
            <div>
                <label for="descripcion">Descripción: </label>
                <input type="text" v-model="tarea.descripcion" id="descripcion">
            </div>
            <div>
                <label for="completada">Completada:</label>
                <input type="checkbox" v-model="tarea.completada" id="completada">
            </div>
            <button type="submit">Actualizar Tarea</button>
         </form>
    </div>
</template>

<script>
import axios from "axios";

const URL = "http://localhost:5180/api/tareas";

export default {
    data(){
        return{

            tarea:{
                nombre:'',
                descripcion:'',
                completada: false
            }
        };
    },

    created() {
        this.obtenerTarea();
    },

    methods: {

        async obtenerTarea(){
            const token = localStorage.getItem("token");

            const id = this.$route.params.id;
            try {
                const response = await axios.get(URL + `/${id}`,{
                    headers: {
                        Authorization: `Bearer ${token}`, // Incluir el token en el encabezado
                    },
                });
                this.tarea = response.data;
                
            } catch (error) {
                console.error('Error al obtener la tarea', error);
            }
        },

        // editar las tarea
        async editarTarea(){
            const token = localStorage.getItem("token");

            const id = this.$route.params.id;

            try {
                await axios.put(URL + `/${id}`, this.tarea, {
                    headers: {
                        Authorization: `Bearer ${token}`, // Incluir el token en el encabezado
                    },
                });
                this.$router.push('/tareas')
                
            } catch (error) {
                console.error('Error al actualizar la tarea', error);
            }
        }

    }
}
</script>