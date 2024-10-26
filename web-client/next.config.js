/** @type {import('next').NextConfig} */
const nextConfig = {
   output: 'standalone',
   env: {
    APP_ENV: process.env.APP_ENV,
  },
   reactStrictMode: false, //Si se tiene en true, se va a llamar dos veces al hook de useEffect
   productionBrowserSourceMaps: false,
    // Optional: Add a trailing slash to all paths `/about` -> `/about/`
    // trailingSlash: true,
    // Optional: Change the output directory `out` -> `dist`
    // distDir: 'dist',

    webpack: (config) => {
      config.externals = [...config.externals, { canvas: "canvas" }];  // required to make Konva & react-konva work
      return config;
    }
  };
   

module.exports = nextConfig
