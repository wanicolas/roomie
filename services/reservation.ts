import { useAuthStore } from "@/store/auth"; // Assure-toi d'avoir un store d'auth
import { useFetch } from "#app";

const API_URL = "http://localhost:5184/api/Reservation";

export async function createReservation(roomId: number, startTime: string, endTime: string) {
  const authStore = useAuthStore();
  
  if (!authStore.token) {
    throw new Error("Utilisateur non authentifié");
  }

  const response = await useFetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${authStore.token}`, // 🔥 Ajout du token JWT
    },
    body: JSON.stringify({ roomId, startTime, endTime }),
  });

  return response;
}
