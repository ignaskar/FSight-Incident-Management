export interface IToken {
  sub?: string;
  email?: string;
  given_name?: string;
  family_name?: string;
  role?: string;
  nbf?: number;
  exp?: number;
  iat?: number;
  iss?: string;
}