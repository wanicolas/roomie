<template>
    <div class="flex flex-col items-center justify-center min-h-screen p-4">
      <Card class="w-full max-w-md p-6 text-center">
        <h1 class="text-2xl font-bold mb-4">Inscription réussie !</h1>
        <p class="mb-4">Vous allez être redirigé vers la page d'accueil...</p>
        <Button @click="$router.push('/')">Retour à l'accueil</Button>
      </Card>
    </div>
  </template>
  
  <script setup>
  import { onMounted } from 'vue';
  import { useRouter } from 'vue-router';
  
  const router = useRouter();
  
  onMounted(() => {
    setTimeout(() => {
      router.push('/');
    }, 3000);
  });
  </script>