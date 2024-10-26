<h1>Readme</h1>

<p>Este proyecto utiliza el framework Next.js con React 18 y el enrutamiento de la aplicación.</p>

<h2>Enrutamiento de páginas</h2>

<p>Las rutas deben configurarse como carpetas dentro de la carpeta "app". Dentro de cada carpeta, se colocará el archivo "page.tsx" junto con sus respectivas subrutas.</p>

<h2>Componentes y estilos</h2>

<h3>Carpeta "components"</h3>

<p>Esta carpeta contiene componentes reutilizables que no contienen lógica de negocio.</p>

<h3>Carpeta "smartcomponents"</h3>

<p>En esta carpeta se encuentran los "smartcomponents" que contienen lógica de negocio. Se recomienda separar la lógica de código de esta manera siempre que sea posible.</p>

<p>Dentro de la carpeta "smartcomponents" hay una subcarpeta llamada "forms". Estos son una clase particular de "smartcomponents" que están diseñados para manejar formularios del sistema. No realizan ninguna petición a la API, solo recopilan o muestran información, y envían la petición para que una página o "smartcontainer" se encargue de la lógica.</p>

<h3>Estilo global</h3>

<ul>
  <li>Para el estilizado global de componentes nativos de HTML y CSS se debe utilizar "global.scss".</li>
  <li>Para el estilizado global de componentes de MUI (Material-UI) se debe utilizar "theme.tsx" en la carpeta "app/".</li>
</ul>

<p>Si se desea estilizar componentes que se reutilizan muchas veces, se debe recurrir a la creación de "styled components". Ejemplos de ellos se encuentran en la carpeta "components/".</p>

<p>No se debe agregar otro framework de UI además de MUI. En caso de requerir una librería externa para implementar alguna funcionalidad, se deben seguir los siguientes pasos:</p>

<ol>
  <li>Verificar que la librería tenga actualizaciones recientes o al menos soporte confirmado.</li>
  <li>Verificar que no tenga dependencias a jQuery.</li>
  <li>En caso de ser posible, utilizar librerías oficiales.</li>
</ol>

<h2>Arquitectura</h2>

<p>Para la arquitectura lógica del sitio, se utilizará el state management con los hooks de React. Se utilizará "useState" para el estado local de los componentes relacionados con la vista y "useReducer" junto con "useEffect" para el estado externo de la aplicación, como llamados a APIs externas.</p>

<h2>Documentación y Referencias</h2>

<ul>
  <li><a href="https://react.dev/">React</a></li>
  <li><a href="https://nextjs.org/">Next.js</a></li>
  <li><a href="https://mui.com/">MUI (Material-UI)</a></li>
</ul>

<h1>NextJS Readme</h1>
This is a [Next.js](https://nextjs.org/) project bootstrapped with [`create-next-app`](https://github.com/vercel/next.js/tree/canary/packages/create-next-app).

## Getting Started

First, run the development server:

```bash
npm run dev
# or
yarn dev
# or
pnpm dev
```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the result.

You can start editing the page by modifying `app/page.tsx`. The page auto-updates as you edit the file.

This project uses [`next/font`](https://nextjs.org/docs/basic-features/font-optimization) to automatically optimize and load Inter, a custom Google Font.

## Learn More

To learn more about Next.js, take a look at the following resources:

- [Next.js Documentation](https://nextjs.org/docs) - learn about Next.js features and API.
- [Learn Next.js](https://nextjs.org/learn) - an interactive Next.js tutorial.

You can check out [the Next.js GitHub repository](https://github.com/vercel/next.js/) - your feedback and contributions are welcome!

## Deploy on Vercel

The easiest way to deploy your Next.js app is to use the [Vercel Platform](https://vercel.com/new?utm_medium=default-template&filter=next.js&utm_source=create-next-app&utm_campaign=create-next-app-readme) from the creators of Next.js.

Check out our [Next.js deployment documentation](https://nextjs.org/docs/deployment) for more details.
