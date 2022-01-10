<template>
  <nav class="navbar navbar-dark bg-dark">
    <div class="container-fluid">
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="offcanvas"
        data-bs-target="#offcanvasNavbar"
        aria-controls="offcanvasNavbar"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div
        class="offcanvas offcanvas-start text-success bg-dark"
        tabindex="-1"
        id="offcanvasNavbar"
        aria-labelledby="offcanvasNavbarLabel"
      >
        <div class="offcanvas-header">
          <h5 class="offcanvas-title" id="offcanvasNavbarLabel">KEEPr</h5>
          <button
            type="button "
            class="btn-close btn-close-white text-reset"
            data-bs-dismiss="offcanvas"
            aria-label="Close"
          ></button>
        </div>
        <div class="offcanvas-body">
          <ul class="navbar-nav justify-content-end flex-grow-1 pe-3">
            <li class="nav-item">
              <h5 @click="routeTo('Home')" class="nav-link">HOME</h5>
            </li>
            <li class="nav-item">
              <h5 @click="routeTo('About')" class="nav-link">ABOUT</h5>
            </li>
            <li
              class="nav-item"
              title="profile page"
              v-if="user.isAuthenticated"
            >
              <h5 @click="routeTo('Account')" class="nav-link">PROFILE</h5>
            </li>
            <li v-if="user.isAuthenticated" class="my-2">
              <img
                :src="user.picture"
                alt="user photo"
                height="40"
                class="rounded"
              />
              <span class="mx-3 text-success lighten-30">{{ user.name }}</span>
            </li>
            <li>
              <button
                class="btn selectable text-success border-success my-2"
                @click="login"
                v-if="!user.isAuthenticated"
              >
                LOGIN
              </button>
              <button
                class="btn selectable text-success border-success my-2"
                v-if="user.isAuthenticated"
                @click="logout"
              >
                LOGOUT
              </button>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { Offcanvas } from 'bootstrap'
export default {
  setup() {
    const router = useRouter()
    return {
      router,
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      },
      routeTo(route) {
        router.push({
          name: route,
        })
        Offcanvas.getOrCreateInstance(document.getElementById("offcanvasNavbar")).hide()
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
