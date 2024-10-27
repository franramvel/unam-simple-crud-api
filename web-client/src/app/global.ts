
interface Configuration {
  API_URL: string;
}

interface Enviroments {
  local: Configuration;
}


const config: Enviroments = {
  local: {
    API_URL: 'https://localhost:7023/',
  },
};
const env = process.env.APP_ENV;

export const API_URL= config[env as keyof Enviroments].API_URL