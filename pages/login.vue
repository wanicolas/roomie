<template>
	<div class="flex flex-col items-center justify-center min-h-screen p-4">
	  <Card class="w-full max-w-md p-6">
		<h1 class="text-2xl font-bold mb-4">{{ isRegister ? 'Inscription' : 'Connexion' }}</h1>
		<form @submit.prevent="submitForm">
		  <Input v-if="isRegister" v-model="form.fullName" placeholder="Nom complet" class="mb-2" required />
		  <Input v-model="form.email" type="email" placeholder="Email" class="mb-2" required />
		  <Input v-model="form.password" type="password" placeholder="Mot de passe" class="mb-4" required />
		  <Button type="submit" class="w-full">{{ isRegister ? "S'inscrire" : "Se connecter" }}</Button>
		</form>
		<p class="mt-4 text-sm text-center">
		  {{ isRegister ? 'Déjà un compte ?' : 'Pas encore de compte ?' }}
		  <span @click="toggleForm" class="text-blue-500 cursor-pointer">
			{{ isRegister ? 'Se connecter' : 'S\'inscrire' }}
		  </span>
		</p>
	  </Card>
	</div>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  
  const router = useRouter();
  const isRegister = ref(false);
  const form = ref({ fullName: '', email: '', password: '' });
  
  const submitForm = async () => {
	if (isRegister.value) {
	  console.log('Inscription :', form.value);
	  router.push('/register-success');
	} else {
	  console.log('Connexion :', form.value);
	  router.push('/');
	}
  };
  
  const toggleForm = () => {
	isRegister.value = !isRegister.value;
  };
  </script>