**Code Challenge Answer to** [ze-code-challenges
/backend](https://github.com/ab-inbev-ze-company/ze-code-challenges/blob/master/backend.md)

#### For testing
- Install PostgreSql with PostGIS extension.
- At "appsettings.json" file, under "ConnectionStrings" section, you can edit the database connection.
- The program uses SDK 8.0
- ---
#### Testing
- You can just run "ZeDeliveryCodeChallenge.exe" and it will be available at it's default connection setting (http://localhost:5000). Also, you can run it at another host/port~~~~ with command below.
  > dotnet ZeDeliveryCodeChallenge.dll --urls http://0.0.0.0:0000

  http://0.0.0.0:0000 should be your custom urls.
---
#### Routes
- Get all partners:
  >http://0.0.0.0:0000/api/Partners
- Get a partner (by id)
  >http://0.0.0.0:0000/api/Partners/{id}
- Post a partner
  >http://0.0.0.0:0000/api/Partners

---
obs: {id} is an string.
~~~~
obs2: Body of post should follow the description of code challenge.