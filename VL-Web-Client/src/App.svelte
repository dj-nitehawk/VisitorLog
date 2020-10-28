<script>
  import TailwindStyles from "./TailwindStyles.svelte";
  const message = "Hello World...";
</script>

<style>
  .App-logo {
    animation: App-logo-scale infinite 0.2s ease-in-out alternate;
  }
  @keyframes App-logo-scale {
    from {
      transform: scale(1);
    }
    to {
      transform: scale(1.06);
    }
  }
</style>

<div class="text-center font-serif">
  <header
    class="bg-gray-100 h-screen flex justify-center items-center flex-col text-3xl">
    <img
      src="logo.svg"
      class="App-logo pointer-events-none m-4 h-64"
      alt="logo" />
    <a
      class="text-green-700"
      href="https://svelte.dev"
      target="_blank"
      rel="noopener noreferrer">
      {message}
    </a>
  </header>
</div>
