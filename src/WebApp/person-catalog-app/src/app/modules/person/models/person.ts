import { GenderStatus } from "./genderStatus";

export interface Person
{
  id? :string,
  fullName ?:string,
  dateOfBirth ?:Date,
  email ?:string,
  phoneNumber ?:string,
  address ?:string,
  gender ?:GenderStatus,
  nationality ?:string,
  occupation ?:string
}
