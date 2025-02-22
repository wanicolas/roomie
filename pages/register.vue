<template>
    <div class="flex flex-col items-center justify-center min-h-screen p-4">
      <Card class="w-full max-w-md p-6">
        <h1 class="text-2xl font-bold mb-4">Inscription</h1>
        <form @submit.prevent="register">
          <Input v-model="form.fullName" placeholder="Nom complet" class="mb-2" required />
          <Input v-model="form.email" type="email" placeholder="Email" class="mb-2" required />
          <Input v-model="form.password" type="password" placeholder="Mot de passe" class="mb-4" required />
          <Button type="submit" class="w-full">S'inscrire</Button>
        </form>
        <p class="mt-4 text-sm text-center">
          Déjà un compte ?
          <NuxtLink to="/login" class="text-blue-500">Se connecter</NuxtLink>
        </p>
      </Card>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  
  const router = useRouter();
  const form = ref({ fullName: '', email: '', password: '' });
  
  const register = async () => {
    try {
      // Simule une inscription (à remplacer par un appel API réel)
      console.log('Utilisateur inscrit :', form.value);
      router.push('/register-success');
    } catch (error) {
      console.error('Erreur lors de l\'inscription', error);
    }
  };
  </script>