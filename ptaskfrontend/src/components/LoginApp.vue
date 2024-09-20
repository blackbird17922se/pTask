<template>
  <div>
    <h1>Login</h1>
    <form @submit.prevent="login">
      <div>
        <label for="nombre">Nombre Usuario</label>
        <input type="text" v-model="nombre" id="nombre" />
      </div>
      <div>
        <label for="password"></label>
        <input type="password" v-model="passwordHash" id="password" />
      </div>
      <button type="submit">Iniciar Sesion</button>
    </form>
  </div>
</template>

<script>
import axios from "axios";
let url = "http://localhost:5180/api/Auth/login";

export default {
  // función que devuelve un objeto donde defines las propiedades reactivas del componente.
    data() {
        return {
            nombre: "",
            password: "",
        };
    },

    // Es un objeto donde defines las funciones que puedes invocar dentro del componente
    methods: {
        async login(){
            try {
                const response = await axios.post(url, {
                    nombre: this.nombre,
                    passwordHash: this.passwordHash
                });

                // Guardar el token en localStorage
                localStorage.setItem('token', response.data.token);
                alert('Login exitos');

                // redirigir a la vista tareas
                this.$router.push('/tareas'); 
                
            } catch (error) {
                alert('erorr al iniciar sesion');
            }
        },
    }
};
</script>

<style scoped>
/* Aquí puedes agregar estilos para el componente */
</style>