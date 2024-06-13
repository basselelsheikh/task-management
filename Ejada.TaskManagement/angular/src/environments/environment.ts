import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'TaskManagement',
    logoUrl: 'assets/logo-light-c.png',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44355/',
    redirectUri: baseUrl,
    clientId: 'TaskManagement_App',
    responseType: 'code',
    scope: 'offline_access TaskManagement',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44355',
      rootNamespace: 'Ejada.TaskManagement',
    },
  },
} as Environment;
