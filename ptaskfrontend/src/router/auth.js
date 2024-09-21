export function isAuthenticated() {
    const token = localStorage.getItem('token');
    if (!token) return false;

    const payload = JSON.parse(atob(token.split('.')[1]));
    const exp = payload.exp * 1000; // Convertir a milisegundos

    return Date.now() < exp; // Verifica si el token no ha expirado
}
