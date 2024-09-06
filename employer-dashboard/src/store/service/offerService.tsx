import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { Offer } from "../../@types/types";

// Define a service using a base URL and expected endpoints
export const juniorHubApi = createApi({
  reducerPath: "juniorHubApi",
  baseQuery: fetchBaseQuery({ baseUrl: "https://pokeapi.co/api/v2/" }),
  endpoints: (builder) => ({
    getPokemonByName: builder.query<Offer, string>({
      query: (name) => `pokemon/${name}`,
    }),
  }),
});

// Export hooks for usage in functional components, which are
// auto-generated based on the defined endpoints
export const { useGetPokemonByNameQuery } = juniorHubApi;
