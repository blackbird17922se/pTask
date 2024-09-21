import {createRouter, createWebHistory} from 'vue-router';
import LoginApp from '@/components/LoginApp.vue';
import TareasApp from '@/components/TareasApp.vue';
import EditarTarea from '@/components/EditarTarea.vue';
import { isAuthenticated } from './auth';

const routes = [
    {
        path: '/login',
        name: 'Login',
        component: LoginApp
    },
    {
        path: '/tareas',
        name: 'Tareas',
        component: TareasApp, // Asegúrate de que TareasApp.vue exista

        // para validar q esta el token activo y dejar o no acceder a la vista
        beforeEnter: (to, from, next) => {
            if (isAuthenticated()) {
                next() //permitir aveso
            } else {
                next('/login')
            }
        }

    },
    // TODO: Retirar si editar no se va a usar
    {
        path: '/tareas/:id/editar',
        name: 'EditarTarea',
        component: EditarTarea // Asegúrate de que TareasApp.vue exista
    },
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

export default router;