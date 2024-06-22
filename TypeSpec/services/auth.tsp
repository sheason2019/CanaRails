import "@typespec/http";
import "@typespec/rest";
import "@typespec/openapi3";

using TypeSpec.Http;
using TypeSpec.Rest;

@service({
  title: "App Auth Service",
})
@server("/api/auth", "App Auth Server endpoint")
@tag("AuthController")
namespace Auth;

model AuthData {
  username: string;
}

op getAuthData(): AuthData;

model LoginReq {
  username: string;
  password: string;
}

model LoginRes {
  jwt: string;
}

@post
op login(@body req: LoginReq): LoginRes;
