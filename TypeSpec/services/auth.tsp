import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

model AuthData {
  username: string;
}

model LoginReq {
  username: string;
  password: string;
}

model LoginRes {
  jwt: string;
}

@tag("AuthController")
@route("/api/auth")
interface Auth {
  getAuthData(): AuthData;

  @post
  login(@body req: LoginReq): LoginRes;
}
