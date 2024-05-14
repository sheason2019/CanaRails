export default function DrawerButton() {
  const DRAWER_ID = "nav-drawer";

  return (
    <div class="drawer">
      <input id={DRAWER_ID} type="checkbox" class="drawer-toggle" />
      <div class="drawer-content">
        <label for={DRAWER_ID} class="btn btn-ghost btn-square drawer-button">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            class="inline-block w-5 h-5 stroke-current"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
              d="M4 6h16M4 12h16M4 18h16"
            ></path>
          </svg>
        </label>
      </div>
      <div class="drawer-side">
        <label
          for={DRAWER_ID}
          aria-label="close sidebar"
          class="drawer-overlay"
        ></label>
        <ul class="menu p-4 w-80 min-h-full bg-base-200 text-base-content">
          <li class="menu-title">导航</li>
          <li>
            <a href="/dashboard">仪表盘</a>
          </li>
          <li>
            <a href="/app">应用</a>
          </li>
        </ul>
      </div>
    </div>
  );
}
