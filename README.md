# Développement local du projet Roomie

Roomie is a room booking wep application.

This project uses Nuxt with NuxtUI and TailwindCSS (itegrated with NuxtUI) for the frontend.
For the backend, it uses a Dotnet8.0 solution with a PostgreSQL database.

Look at the [Nuxt documentation](https://nuxt.com/docs/getting-started/introduction) to learn more.
Look at the[Dotnet documentation] (https://learn.microsoft.com/en-us/dotnet) to learn more

## Frontend setup

Make sure to install dependencies:

```bash
# npm
npm install

# pnpm
pnpm install

# yarn
yarn install

# bun
bun install
```

## Development Server

Start the development server on `http://localhost:3000`:

```bash
# npm
npm run dev

# pnpm
pnpm dev

# yarn
yarn dev

# bun
bun run dev
```

## Production

Build the application for production:

```bash
# npm
npm run build

# pnpm
pnpm build

# yarn
yarn build

# bun
bun run build
```

Locally preview production build:

```bash
# npm
npm run preview

# pnpm
pnpm preview

# yarn
yarn preview

# bun
bun run preview
```

Check out the [deployment documentation](https://nuxt.com/docs/getting-started/deployment) for more information.

The frontend makes its request on port 5184 as it is the one Dotnet uses.

## Backend setup

Make sure to restore the solution:

```bash
dotnet restore
```

Run the solution on `http://localhost:5184`:

```bash
dotnet run
```

## Use the application

Admin connexion:
"email": "admin@roomie.com",
"password": "Admin123!"

When connected as an admin, you can visit `http://localhost:3000/admin` to read, update, create or delete rooms.

To book a room, you can register then login with the links in the header.
After the login, you can search for an available room and book it. It will then be shown in you account profile page.
