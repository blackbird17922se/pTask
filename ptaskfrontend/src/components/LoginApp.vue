<template>
  <div class="flex items-center justify-center min-h-screen bg-black">
    <div class="bg-gray-800 bg-opacity-50 rounded-lg shadow-lg p-8">
        <h1 class="text-3xl text-center mb-6 logo-login">PTask</h1>
        <div class="box-form">
          <form @submit.prevent="login">
            <div class="mb-4">
              <label for="nombre" >Nombre Usuario</label>
              <input type="text" v-model="nombre" id="nombre" 
              class="mt-1 block w-full p-2 border rounded" 
              @focus="mensajeError = ''"/>
            </div>
            <div class="mb-4">
              <label for="password">Contraseña</label>
              <input type="password" v-model="passwordHash" 
              id="password" class="mt-1 block w-full p-2 border rounded"
              @focus="mensajeError = ''"/>
            </div>
            <button type="submit" 
            class="w-full bg-gray-600 text-white py-2 rounded hover:bg-gray-700"
            >Iniciar Sesion</button>
            <p v-if="mensajeError" class="text-red-500 mt-2">{{ mensajeError }}</p>
          </form>

        </div>
    </div>
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
      passwordHash: "",
      mensajeError: ""
    };
  },

  // Es un objeto donde defines las funciones que puedes invocar dentro del componente
  methods: {
    async login() {
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
        this.mensajeError = 'Credenciales incorrectas';
      }
    },
  }
};
</script>

<style scoped>
/* 
LOG-IN 
Rfood DsD 2021
Mauricio Alarcon
*/

.logo-login {
  color: rgb(255 255 255 / 58%);
  font-family: Verdana, Geneva, Tahoma, sans-serif;
  font-weight: bolder;
  font-style: italic;
}

.box-form {
  background: #ffffffeb;
  backdrop-filter: blur(20px);
  padding: 30px;
  border-radius: 5px;
  border-color: #d9d9d9;
  border-width: 2px;
  border-style: solid;
}

</style>