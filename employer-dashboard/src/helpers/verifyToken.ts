import { decodeJwt, JWTPayload } from "jose";

export const verifyToken = async (
  token: string
): Promise<JWTPayload | null> => {
  try {
    const user = decodeJwt(token);

    return user;
  } catch (error) {
    console.error("Token inválido o expirado");
    return null;
  }
};
