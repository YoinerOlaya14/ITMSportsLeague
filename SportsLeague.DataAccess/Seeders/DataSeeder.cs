using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;

namespace SportsLeague.DataAccess.Seeders;

public static class DataSeeder
{
    public static async Task SeedAsync(LeagueDbContext context)
    {
        // Solo ejecutar si no hay equipos (BD vacía)
        if (await context.Teams.AnyAsync()) return;

        // ═══ 1. EQUIPOS (Liga BetPlay 2026) ═══
        var teams = new List<Team>
        {
            new() { Name="Atlético Nacional", City="Medellín", Stadium="Atanasio Girardot" },
            new() { Name="Independiente Medellín", City="Medellín", Stadium="Atanasio Girardot" },
            new() { Name="América de Cali", City="Cali", Stadium="Pascual Guerrero" },
            new() { Name="Deportivo Cali", City="Cali", Stadium="Deportivo Cali" },
            new() { Name="Junior FC", City="Barranquilla", Stadium="Metropolitano" },
            new() { Name="Millonarios FC", City="Bogotá", Stadium="El Campín" },
            new() { Name="Independiente Santa Fe", City="Bogotá", Stadium="El Campín" },
            new() { Name="Deportes Tolima", City="Ibagué", Stadium="Manuel Murillo Toro" },
            new() { Name="Atlético Bucaramanga", City="Bucaramanga", Stadium="Alfonso López" },
            new() { Name="Once Caldas", City="Manizales", Stadium="Palogrande" },
            new() { Name="Deportivo Pasto", City="Pasto", Stadium="Departamental Libertad" },
            new() { Name="Deportivo Pereira", City="Pereira", Stadium="Hernán Ramírez Villegas" },
            new() { Name="Águilas Doradas", City="Rionegro", Stadium="Alberto Grisales" },
            new() { Name="Boyacá Chicó FC", City="Tunja", Stadium="La Independencia" },
            new() { Name="Jaguares de Córdoba", City="Montería", Stadium="Jaraguay" },
            new() { Name="Alianza Valledupar FC", City="Valledupar", Stadium="Armando Maestre" },
            new() { Name="Fortaleza FC", City="Bogotá", Stadium="Metropolitano de Techo" },
            new() { Name="Llaneros FC", City="Villavicencio", Stadium="Bello Horizonte" },
            new() { Name="Cúcuta Deportivo", City="Cúcuta", Stadium="General Santander" },
            new() { Name="Internacional de Bogotá", City="Bogotá", Stadium="Metropolitano de Techo" },
        };

        context.Teams.AddRange(teams);
        await context.SaveChangesAsync();

        // ═══ 2. JUGADORES (15 por equipo = 300 total) ═══
        var playersData = new (string First, string Last, PlayerPosition Pos, int Number)[][]
        {
            // 1. Atlético Nacional
            new[] {
                   ("David", "Ospina", PlayerPosition.Goalkeeper, 1),  //ID 1
                    ("Kevin", "Mier", PlayerPosition.Goalkeeper, 12),  //ID 2
                    ("William", "Tesillo", PlayerPosition.Defender, 3), //ID 3
                    ("Cristian", "Zapata", PlayerPosition.Defender, 2), //ID 4
                    ("Juan Felipe", "Aguirre", PlayerPosition.Defender, 5), //ID 5
                    ("Andrés", "Román", PlayerPosition.Defender, 13), //ID 6
                    ("Nelson", "Palacios", PlayerPosition.Midfielder, 6), //ID 7
                    ("Sebastián", "Gómez", PlayerPosition.Midfielder, 8), //ID 8
                    ("Edwin", "Cardona", PlayerPosition.Midfielder, 10), //ID 9
                    ("Dorlan", "Pabón", PlayerPosition.Midfielder, 7), //ID 10
                    ("Jhon", "Duque", PlayerPosition.Midfielder, 14), //ID 11
                    ("Andrés", "Salazar", PlayerPosition.Defender, 26), //ID 12
                    ("Alfredo", "Morelos", PlayerPosition.Forward, 9), //ID 13
                    ("Eric", "Ramírez", PlayerPosition.Forward, 17), //ID 14
                    ("Óscar", "Perea", PlayerPosition.Forward, 30), //ID 15
            },

            // 2. Independiente Medellín
            new[] {
                    ("Salvador", "Ichazo", PlayerPosition.Goalkeeper, 1), // ID 16
                    ("Andrés", "Mosquera Marmolejo", PlayerPosition.Goalkeeper, 12), // ID 17
                    ("Andrés", "Cadavid", PlayerPosition.Defender, 4), // ID 18
                    ("Jhon", "Palacios", PlayerPosition.Defender, 2), // ID 19
                    ("Yulián", "Gómez", PlayerPosition.Defender, 26), // ID 20
                    ("Daniel", "Londoño", PlayerPosition.Defender, 13), // ID 21
                    ("Adrián", "Arregui", PlayerPosition.Midfielder, 5), // ID 22
                    ("Didier", "Bueno", PlayerPosition.Midfielder, 6), // ID 23
                    ("Jhon", "Murillo", PlayerPosition.Midfielder, 7), // ID 24
                    ("Miguel", "Monsalve", PlayerPosition.Midfielder, 10), // ID 25
                    ("Brayan", "López", PlayerPosition.Midfielder, 14), // ID 26
                    ("Luciano", "Pons", PlayerPosition.Forward, 9), // ID 27
                    ("Ever", "Valencia", PlayerPosition.Forward, 17), // ID 28
                    ("Diego", "Moreno", PlayerPosition.Forward, 19), // ID 29
                    ("Jordy", "Monroy", PlayerPosition.Defender, 22), // ID 30
            },
            // 3. América de Cali
            new[] {
                    ("Joel", "Graterol", PlayerPosition.Goalkeeper, 1), // ID 31
                    ("Diego", "Novoa", PlayerPosition.Goalkeeper, 12), // ID 32
                    ("Jorge", "Segura", PlayerPosition.Defender, 3), // ID 33
                    ("Kevin", "Andrade", PlayerPosition.Defender, 2), // ID 34
                    ("Esneyder", "Mena", PlayerPosition.Defender, 27), // ID 35
                    ("Daniel", "Bocanegra", PlayerPosition.Defender, 5), // ID 36
                    ("Rodrigo", "Ureña", PlayerPosition.Midfielder, 8), // ID 37
                    ("Luis", "Paz", PlayerPosition.Midfielder, 6), // ID 38
                    ("Cristian", "Barrios", PlayerPosition.Midfielder, 11), // ID 39
                    ("Edwin", "Velasco", PlayerPosition.Midfielder, 22), // ID 40
                    ("Adrián", "Ramos", PlayerPosition.Forward, 9), // ID 41
                    ("Andrés", "Sarmiento", PlayerPosition.Forward, 17), // ID 42
                    ("Iago", "Falque", PlayerPosition.Forward, 10), // ID 43
                    ("Jader", "Quiñones", PlayerPosition.Midfielder, 14), // ID 44
                    ("Facundo", "Suárez", PlayerPosition.Forward, 19), // ID 45
            },
            // 4. Deportivo Cali
            new[] {
                    ("Pedro", "Gallese", PlayerPosition.Goalkeeper, 1), // ID 46
                    ("Humberto", "Acevedo", PlayerPosition.Goalkeeper, 12), // ID 47
                    ("Fernando", "Álvarez", PlayerPosition.Defender, 4), // ID 48
                    ("Germán", "Mera", PlayerPosition.Defender, 2), // ID 49
                    ("Juan", "Franco", PlayerPosition.Defender, 27), // ID 50
                    ("Yerson", "Candelo", PlayerPosition.Defender, 13), // ID 51
                    ("Kevin", "Velasco", PlayerPosition.Midfielder, 10), // ID 52
                    ("Jhon", "Vásquez", PlayerPosition.Midfielder, 7), // ID 53
                    ("Andrés", "Balanta", PlayerPosition.Midfielder, 6), // ID 54
                    ("Fabry", "Castillo", PlayerPosition.Midfielder, 14), // ID 55
                    ("Juan", "Dinenno", PlayerPosition.Forward, 9), // ID 56
                    ("Luis", "Sandoval", PlayerPosition.Forward, 19), // ID 57
                    ("Jhon", "Murillo", PlayerPosition.Forward, 17), // ID 58   
                    ("Harold", "Preciado", PlayerPosition.Forward, 21), // ID 59
                    ("Yeison", "Tolosa", PlayerPosition.Midfielder, 22), // ID 60
            },
            // 5. Junior FC
            new[] {
                    ("Mauro", "Silveira", PlayerPosition.Goalkeeper, 1), // ID 61
                    ("Jefferson", "Martínez", PlayerPosition.Goalkeeper, 12), // ID 62
                    ("Edwin", "Herrera", PlayerPosition.Defender, 4), // ID 63
                    ("Homer", "Martínez", PlayerPosition.Defender, 5), // ID 64
                    ("Willer", "Ditta", PlayerPosition.Defender, 3), // ID 65
                    ("Gabriel", "Fuentes", PlayerPosition.Defender, 20), // ID 66
                    ("Fabián", "Ángel", PlayerPosition.Midfielder, 8), // ID 67
                    ("Víctor", "Cantillo", PlayerPosition.Midfielder, 24), // ID 68
                    ("Yimmi", "Chará", PlayerPosition.Midfielder, 21), // ID 69
                    ("José", "Enamorado", PlayerPosition.Midfielder, 10), // ID 70
                    ("Carlos", "Bacca", PlayerPosition.Forward, 7), // ID 71
                    ("Steven", "Rodríguez", PlayerPosition.Forward, 19), // ID 72
                    ("Bryan", "Castrillón", PlayerPosition.Forward, 17), // ID 73
                    ("Déiber", "Caicedo", PlayerPosition.Forward, 11), // ID 74
                    ("Johan", "Bocanegra", PlayerPosition.Midfielder, 14), // ID 75
            },
            // 6. Millonarios FC
            new[] {
                    ("Guillermo", "De Amores", PlayerPosition.Goalkeeper, 1), // ID 76
                    ("Álvaro", "Montero", PlayerPosition.Goalkeeper, 12), // ID 77
                    ("Omar", "Bertel", PlayerPosition.Defender, 4), // ID 78
                    ("Andrés", "Llinás", PlayerPosition.Defender, 26), // ID 79
                    ("Jorge", "Arias", PlayerPosition.Defender, 3), // ID 80
                    ("Delvin", "Alfonzo", PlayerPosition.Defender, 13), // ID 81
                    ("Daniel", "Cataño", PlayerPosition.Midfielder, 10), // ID 82
                    ("Larry", "Vásquez", PlayerPosition.Midfielder, 5), // ID 83
                    ("Juan Carlos", "Pereira", PlayerPosition.Midfielder, 8), // ID 84
                    ("David", "Mackalister Silva", PlayerPosition.Midfielder, 14), // ID 85
                    ("Leonardo", "Castro", PlayerPosition.Forward, 9), // ID 86
                    ("Santiago", "Gómez", PlayerPosition.Forward, 19), // ID 87
                    ("Emerson", "Rodríguez", PlayerPosition.Forward, 11), // ID 88
                    ("Yuber", "Quiñones", PlayerPosition.Forward, 17), // ID 89
                    ("Samuel", "Asprilla", PlayerPosition.Defender, 22), // ID 90
            },
            // 7. Independiente Santa Fe
            new[] {
                    ("Leandro", "Castellanos", PlayerPosition.Goalkeeper, 1), // ID 91
                    ("Antony", "Silva", PlayerPosition.Goalkeeper, 12), // ID 92
                    ("Elvis", "Mosquera", PlayerPosition.Defender, 3), // ID 93
                    ("José", "Aja", PlayerPosition.Defender, 4), // ID 94
                    ("Dairon", "Mosquera", PlayerPosition.Defender, 26), // ID 95
                    ("Iván", "Rojas", PlayerPosition.Defender, 13), // ID 96
                    ("Daniel", "Giraldo", PlayerPosition.Midfielder, 5), // ID 97
                    ("Kelvin", "Osorio", PlayerPosition.Midfielder, 10), // ID 98
                    ("Neyder", "Moreno", PlayerPosition.Midfielder, 7), // ID 99
                    ("Fabián", "Sambueza", PlayerPosition.Midfielder, 11), // ID 100
                    ("Hugo", "Rodallega", PlayerPosition.Forward, 9), // ID 101
                    ("Ever", "Valencia", PlayerPosition.Forward, 17), // ID 102
                    ("Jersson", "González", PlayerPosition.Forward, 19), // ID 103
                    ("Cristian", "Marrugo", PlayerPosition.Midfielder, 14), // ID 104
                    ("José", "Enamorado", PlayerPosition.Forward, 21), // ID 105
            },
            // 8. Deportes Tolima
            new[] {
                    ("William", "Cuesta", PlayerPosition.Goalkeeper, 1), // ID 106
                    ("Neto", "Volpi", PlayerPosition.Goalkeeper, 12), // ID 107
                    ("Jersson", "González", PlayerPosition.Defender, 3), // ID 108
                    ("Sergio", "Mosquera", PlayerPosition.Defender, 4), // ID 109
                    ("Junior", "Hernández", PlayerPosition.Midfielder, 10), // ID 110
                    ("Juan David", "Ríos", PlayerPosition.Midfielder, 6), // ID 111
                    ("Bryan", "Gil", PlayerPosition.Midfielder, 8), // ID 112
                    ("Yeison", "Guzmán", PlayerPosition.Midfielder, 11), // ID 113
                    ("Alexis", "Castro", PlayerPosition.Midfielder, 14), // ID 114
                    ("Tatay", "Torres", PlayerPosition.Forward, 9), // ID 115
                    ("Jason", "Guzmán", PlayerPosition.Forward, 17), // ID 116
                    ("Luis", "Miranda", PlayerPosition.Forward, 19), // ID 117
                    ("Kevin", "Pérez", PlayerPosition.Forward, 21), // ID 118
                    ("Juan", "Pablo Nieto", PlayerPosition.Midfielder, 22), // ID 119
                    ("Anderson", "Angulo", PlayerPosition.Defender, 5), // ID 120
            },
            // 9. Atlético Bucaramanga
            new[] {
                    ("Juan Camilo", "Chaverra", PlayerPosition.Goalkeeper, 1), // ID 121
                    ("Aldair", "Quintana", PlayerPosition.Goalkeeper, 12), // ID 122
                    ("José", "Ortiz", PlayerPosition.Defender, 4), // ID 123
                    ("Francisco", "Meza", PlayerPosition.Defender, 3), // ID 124
                    ("Cristian", "Subero", PlayerPosition.Defender, 22), // ID 125
                    ("Jhon", "Harold Gómez", PlayerPosition.Defender, 13), // ID 126
                    ("Sherman", "Cárdenas", PlayerPosition.Midfielder, 10), // ID 127
                    ("Auli", "Oliveros", PlayerPosition.Midfielder, 7), // ID 128
                    ("Víctor", "Guzmán", PlayerPosition.Midfielder, 6), // ID 129
                    ("Diego", "Chica", PlayerPosition.Midfielder, 14), // ID 130
                    ("Sebastián", "Pons", PlayerPosition.Forward, 9), // ID 131
                    ("Johan", "Caballero", PlayerPosition.Forward, 19), // ID 132
                    ("Daniel", "Mosquera", PlayerPosition.Forward, 17), // ID 133
                    ("Cristian", "Flórez", PlayerPosition.Defender, 5), // ID 134
                    ("Kevin", "Londoño", PlayerPosition.Midfielder, 8), // ID 135
            },
            // 10. Once Caldas
            new[] {
                    ("Gerardo", "Ortiz", PlayerPosition.Goalkeeper, 1), // ID 136
                    ("Eder", "Chaux", PlayerPosition.Goalkeeper, 12), // ID 137
                    ("Edisson", "Palomino", PlayerPosition.Defender, 3), // ID 138
                    ("David", "Murillo", PlayerPosition.Defender, 4), // ID 139
                    ("Fáiner", "Torijano", PlayerPosition.Defender, 26), // ID 140
                    ("Julián", "Quiñones", PlayerPosition.Defender, 13), // ID 141
                    ("Sebastián", "Gómez", PlayerPosition.Midfielder, 5), // ID 142
                    ("Juan David", "Pérez", PlayerPosition.Midfielder, 7), // ID 143
                    ("Johan", "Valencia", PlayerPosition.Midfielder, 14), // ID 144
                    ("Billy", "Arce", PlayerPosition.Midfielder, 11), // ID 145
                    ("Dayro", "Moreno", PlayerPosition.Forward, 9), // ID 146
                    ("Marco", "Pérez", PlayerPosition.Forward, 19), // ID 147
                    ("Alejandro", "García", PlayerPosition.Forward, 17), // ID 148
                    ("Harrison", "Otálvaro", PlayerPosition.Midfielder, 10), // ID 149
                    ("Luis", "Payares", PlayerPosition.Defender, 15), // ID 150
            },
            // 11. Deportivo Pasto
            new[] {
                    ("Diego", "Martínez", PlayerPosition.Goalkeeper, 1), // ID 151
                    ("Luis", "Delgado", PlayerPosition.Goalkeeper, 12), // ID 152
                    ("Camilo", "Ayala", PlayerPosition.Defender, 4), // ID 153
                    ("Gilberto", "García", PlayerPosition.Defender, 3), // ID 154
                    ("Kevin", "Riascos", PlayerPosition.Defender, 26), // ID 155
                    ("Jeison", "Quiñones", PlayerPosition.Defender, 13), // ID 156
                    ("Ray", "Vanegas", PlayerPosition.Midfielder, 10), // ID 157
                    ("Jhon", "Sánchez", PlayerPosition.Midfielder, 7), // ID 158
                    ("Duván", "Sánchez", PlayerPosition.Midfielder, 6), // ID 159
                    ("Facundo", "Boné", PlayerPosition.Midfielder, 14), // ID 160
                    ("Jown", "Cardona", PlayerPosition.Forward, 9), // ID 161
                    ("Marvin", "Vallecilla", PlayerPosition.Forward, 19), // ID 162
                    ("Andrés", "Ibargüen", PlayerPosition.Forward, 17), // ID 163
                    ("Daniel", "Luna", PlayerPosition.Midfielder, 11), // ID 164
                    ("Kevin", "López", PlayerPosition.Defender, 5), // ID 165
            },
            // 12. Deportivo Pereira
            new[] {
                    ("Harlen", "Castillo", PlayerPosition.Goalkeeper, 1), // ID 166
                    ("Aldair", "Quintana", PlayerPosition.Goalkeeper, 12), // ID 167
                    ("David", "González", PlayerPosition.Defender, 3), // ID 168
                    ("Carlos", "Ramírez", PlayerPosition.Defender, 4), // ID 169
                    ("Juan Sebastián", "Quintero", PlayerPosition.Defender, 26), // ID 170
                    ("Yilmar", "Velásquez", PlayerPosition.Midfielder, 6), // ID 171
                    ("Brayan", "León", PlayerPosition.Midfielder, 8), // ID 172
                    ("Jimer", "Fajardo", PlayerPosition.Midfielder, 14), // ID 173
                    ("Jhon", "Pajoy", PlayerPosition.Midfielder, 11), // ID 174
                    ("Jonier", "Mosquera", PlayerPosition.Forward, 9), // ID 175
                    ("Ángelo", "Rodríguez", PlayerPosition.Forward, 19), // ID 176
                    ("Arley", "Rodríguez", PlayerPosition.Forward, 17), // ID 177
                    ("Juan", "Zuluaga", PlayerPosition.Defender, 13), // ID 178
                    ("Yesus", "Cabrera", PlayerPosition.Midfielder, 10), // ID 179
                    ("Ever", "Valencia", PlayerPosition.Forward, 21), // ID 180
            },
            // 13. Águilas Doradas
            new[] {
                    ("José Fernando", "Cuadrado", PlayerPosition.Goalkeeper, 1), // ID 181
                    ("Juan David", "Valencia", PlayerPosition.Goalkeeper, 12), // ID 182
                    ("Éder", "Chaux", PlayerPosition.Defender, 4), // ID 183
                    ("Mateo", "Puerta", PlayerPosition.Defender, 3), // ID 184
                    ("Johan", "Martínez", PlayerPosition.Defender, 26), // ID 185
                    ("Juan Pablo", "Otálvaro", PlayerPosition.Defender, 13), // ID 186
                    ("Juan Pablo", "Ramírez", PlayerPosition.Midfielder, 10), // ID 187
                    ("Fredy", "Salazar", PlayerPosition.Midfielder, 7), // ID 188
                    ("Jhon", "Salazar", PlayerPosition.Midfielder, 6), // ID 189
                    ("Kevin", "Castaño", PlayerPosition.Midfielder, 14), // ID 190
                    ("Cristian", "Subero", PlayerPosition.Forward, 9), // ID 191
                    ("Marco", "Pérez", PlayerPosition.Forward, 19), // ID 192
                    ("Johan", "Caballero", PlayerPosition.Forward, 17), // ID 193
                    ("Julián", "Zuluaga", PlayerPosition.Midfielder, 11), // ID 194
                    ("Juan", "Camilo Salazar", PlayerPosition.Forward, 21), // ID 195
            },
            // 14. Boyacá Chicó FC
            new[] {
                    ("Ernesto", "Hernández", PlayerPosition.Goalkeeper, 1), // ID 196
                    ("Pablo", "Mina", PlayerPosition.Goalkeeper, 12), // ID 197
                    ("Carlos", "Henao", PlayerPosition.Defender, 3), // ID 198
                    ("Jhojan", "Gómez", PlayerPosition.Defender, 4), // ID 199
                    ("Henry", "Plazas", PlayerPosition.Defender, 26), // ID 200
                    ("Kelvin", "Osorio", PlayerPosition.Midfielder, 8), // ID 201
                    ("Brayan", "Moreno", PlayerPosition.Midfielder, 14), // ID 202
                    ("Luis", "Mina", PlayerPosition.Midfielder, 6), // ID 203
                    ("Wilmar", "Gómez", PlayerPosition.Midfielder, 11), // ID 204
                    ("Juan David", "Valencia", PlayerPosition.Forward, 9), // ID 205
                    ("José", "Carabalí", PlayerPosition.Forward, 17), // ID 206
                    ("Johan", "Bocanegra", PlayerPosition.Forward, 19), // ID 207
                    ("Duván", "Mosquera", PlayerPosition.Defender, 5), // ID 208
                    ("Jhon", "Arango", PlayerPosition.Midfielder, 7), // ID 209
                    ("Kevin", "López", PlayerPosition.Forward, 21), // ID 210
            },
            // 15. Jaguares de Córdoba
            new[] {
                    ("Diego", "Novoa", PlayerPosition.Goalkeeper, 1), // ID 211
                    ("José", "Escobar", PlayerPosition.Goalkeeper, 12), // ID 212
                    ("Geovan", "Montes", PlayerPosition.Defender, 4), // ID 213
                    ("Yulián", "Anchico", PlayerPosition.Defender, 3), // ID 214
                    ("Jhon", "Viveros", PlayerPosition.Defender, 26), // ID 215
                    ("Larry", "Vásquez", PlayerPosition.Midfielder, 5), // ID 216
                    ("Yeison", "Guzmán", PlayerPosition.Midfielder, 8), // ID 217
                    ("Jhon", "Paz", PlayerPosition.Midfielder, 11), // ID 218
                    ("Sebastián", "Ayala", PlayerPosition.Midfielder, 14), // ID 219
                    ("Pablo", "Bueno", PlayerPosition.Forward, 9), // ID 220
                    ("José", "Rodríguez", PlayerPosition.Forward, 17), // ID 221
                    ("Kevin", "Riascos", PlayerPosition.Forward, 19), // ID 222
                    ("Cristian", "Arrieta", PlayerPosition.Defender, 22), // ID 223
                    ("Juan", "Camilo Roa", PlayerPosition.Midfielder, 6), // ID 224
                    ("Dany", "Rosero", PlayerPosition.Defender, 2), // ID 225
            },
            // 16. Alianza Valledupar FC
            new[] {
                    ("Luis", "Delgado", PlayerPosition.Goalkeeper, 1), // ID 226
                    ("Jorge", "Serrano", PlayerPosition.Goalkeeper, 12), // ID 227
                    ("Marvin", "Vallecilla", PlayerPosition.Defender, 3), // ID 228
                    ("Jefry", "Díaz", PlayerPosition.Defender, 4), // ID 229
                    ("Juan", "Sánchez", PlayerPosition.Midfielder, 8), // ID 230
                    ("Cristian", "Flórez", PlayerPosition.Midfielder, 6), // ID 231
                    ("Jhon", "Flórez", PlayerPosition.Midfielder, 14), // ID 232
                    ("Yeison", "Tolosa", PlayerPosition.Midfielder, 11), // ID 233
                    ("Jeison", "Medina", PlayerPosition.Forward, 9), // ID 234
                    ("Duván", "Vergara", PlayerPosition.Forward, 17), // ID 235
                    ("Kevin", "Londoño", PlayerPosition.Forward, 19), // ID 236
                    ("Luis", "Payares", PlayerPosition.Defender, 5), // ID 237
                    ("Juan", "Camilo Salazar", PlayerPosition.Forward, 21), // ID 238
                    ("Carlos", "Sierra", PlayerPosition.Midfielder, 10), // ID 239
                    ("Jhon", "Pérez", PlayerPosition.Defender, 22), // ID 240
            },
            // 17. Fortaleza FC
            new[] {
                    ("Carlos", "Mosquera", PlayerPosition.Goalkeeper, 1), // ID 241
                    ("Juan", "Mejía", PlayerPosition.Goalkeeper, 12), // ID 242
                    ("Nicolás", "Giraldo", PlayerPosition.Defender, 4), // ID 243
                    ("Andrés", "Correa", PlayerPosition.Defender, 3), // ID 244
                    ("Jhon", "Puerta", PlayerPosition.Midfielder, 10), // ID 245
                    ("Julián", "Gómez", PlayerPosition.Midfielder, 8), // ID 246
                    ("Jhon", "Solís", PlayerPosition.Midfielder, 6), // ID 247
                    ("Kevin", "Londoño", PlayerPosition.Midfielder, 14), // ID 248
                    ("Óscar", "Vanegas", PlayerPosition.Forward, 9), // ID 249
                    ("Yilmar", "Filigrana", PlayerPosition.Forward, 17), // ID 250
                    ("Juan", "Pablo Otálvaro", PlayerPosition.Forward, 19), // ID 251
                    ("Cristian", "Marrugo", PlayerPosition.Midfielder, 11), // ID 252
                    ("Daniel", "Mantilla", PlayerPosition.Midfielder, 7), // ID 253
                    ("Luis", "Anaya", PlayerPosition.Defender, 22), // ID 254
                    ("Mateo", "Puerta", PlayerPosition.Defender, 5), // ID 255
            },
            // 18. Llaneros FC
            new[] {
                    ("José Huber", "Escobar", PlayerPosition.Goalkeeper, 1), // ID 256
                    ("Luis", "Aguirre", PlayerPosition.Goalkeeper, 12), // ID 257
                    ("Cristian", "Arrieta", PlayerPosition.Defender, 3), // ID 258
                    ("Jhon", "Arboleda", PlayerPosition.Defender, 4), // ID 259
                    ("Jhon", "Perea", PlayerPosition.Midfielder, 8), // ID 260
                    ("Juan", "Camilo Portilla", PlayerPosition.Midfielder, 6), // ID 261
                    ("Duván", "Sánchez", PlayerPosition.Midfielder, 14), // ID 262
                    ("Juan Sebastián", "Herrera", PlayerPosition.Forward, 11), // ID 263
                    ("Brayan", "Gil", PlayerPosition.Forward, 9), // ID 264
                    ("Edwar", "López", PlayerPosition.Forward, 17), // ID 265
                    ("Jhon", "Arias", PlayerPosition.Forward, 19), // ID 266
                    ("Kevin", "Pérez", PlayerPosition.Forward, 21), // ID 267
                    ("Luis", "Miranda", PlayerPosition.Forward, 22), // ID 268
                    ("Mateo", "Córdoba", PlayerPosition.Defender, 5), // ID 269
                    ("Juan", "Zuluaga", PlayerPosition.Defender, 13), // ID 270
            },
            // 19. Cúcuta Deportivo
            new[] {
                    ("Norberto", "Araujo", PlayerPosition.Goalkeeper, 1), // ID 271
                    ("Juan", "Chaverra", PlayerPosition.Goalkeeper, 12), // ID 272
                    ("Jefry", "Díaz", PlayerPosition.Defender, 4), // ID 273
                    ("Brayan", "Córdoba", PlayerPosition.Defender, 3), // ID 274
                    ("Mauricio", "Castaño", PlayerPosition.Defender, 26), // ID 275
                    ("Luis", "Miller", PlayerPosition.Defender, 13), // ID 276
                    ("Juan Camilo", "Portilla", PlayerPosition.Midfielder, 10), // ID 277
                    ("Henry", "Rojas", PlayerPosition.Midfielder, 6), // ID 278
                    ("Jhon", "Hernández", PlayerPosition.Midfielder, 8), // ID 279
                    ("Kevin", "Salazar", PlayerPosition.Midfielder, 14), // ID 280
                    ("Edwar", "López", PlayerPosition.Forward, 9), // ID 281
                    ("Jhon", "Jairo Pérez", PlayerPosition.Forward, 17), // ID 282
                    ("Dany", "Cano", PlayerPosition.Forward, 19), // ID 283
                    ("Carlos", "Sinuco", PlayerPosition.Midfielder, 11), // ID 284
                    ("Julián", "Guerrero", PlayerPosition.Defender, 5), // ID 285
            },
            // 20. Internacional de Bogotá
            new[] {
                    ("Neto", "Volpi", PlayerPosition.Goalkeeper, 1), // ID 286
                    ("Luis", "Chávez", PlayerPosition.Goalkeeper, 12), // ID 287
                    ("Nicolás", "Hernández", PlayerPosition.Defender, 3), // ID 288
                    ("Juan", "Mosquera", PlayerPosition.Defender, 4), // ID 289
                    ("Andrés", "Murillo", PlayerPosition.Defender, 26), // ID 290
                    ("Jhon", "Banguero", PlayerPosition.Defender, 13), // ID 291
                    ("Carlos Darwin", "Quintero", PlayerPosition.Midfielder, 10), // ID 292
                    ("Jhon", "Velásquez", PlayerPosition.Midfielder, 8), // ID 293
                    ("Cristian", "Arango", PlayerPosition.Midfielder, 6), // ID 294
                    ("Daniel", "Ruiz", PlayerPosition.Midfielder, 14), // ID 295
                    ("Facundo", "Boné", PlayerPosition.Forward, 9), // ID 296
                    ("Diego", "Herazo", PlayerPosition.Forward, 17), // ID 297
                    ("Jhon", "Fredy Miranda", PlayerPosition.Forward, 19), // ID 298
                    ("Kevin", "Castaño", PlayerPosition.Midfielder, 11), // ID 299
                    ("Luis", "Perea", PlayerPosition.Defender, 5), // ID 300
            },
        };

        var players = new List<Player>();
        for (int i = 0; i < teams.Count; i++)
        {
            foreach (var pd in playersData[i])
            {
                players.Add(new Player
                {
                    FirstName = pd.First,
                    LastName = pd.Last,
                    Number = pd.Number,
                    Position = pd.Pos,
                    BirthDate = new DateTime(1995, 1, 1).AddMonths(players.Count),
                    TeamId = teams[i].Id
                });
            }
        }
        context.Players.AddRange(players);
        await context.SaveChangesAsync();

        // ═══ 3. ÁRBITROS ═══
        var referees = new List<Referee>
        {
            new() { FirstName="Wilmar", LastName="Roldán", Nationality="Colombia" },
            new() { FirstName="Andrés", LastName="Rojas", Nationality="Colombia" },
            new() { FirstName="Carlos", LastName="Betancur", Nationality="Colombia" },
            new() { FirstName="Jhon", LastName="Hinestroza", Nationality="Colombia" },
        };
        context.Referees.AddRange(referees);
        await context.SaveChangesAsync();

        // ═══ 4. TORNEO ═══
        var tournament = new Tournament
        {
            Name = "Liga BetPlay 2026-I",
            Season = "2026-I",
            StartDate = new DateTime(2026, 1, 16),
            EndDate = new DateTime(2026, 6, 5),
            Status = TournamentStatus.InProgress
        };
        context.Tournaments.Add(tournament);
        await context.SaveChangesAsync();

        // ═══ 5. INSCRIBIR LOS 20 EQUIPOS ═══
        foreach (var team in teams)
        {
            context.TournamentTeams.Add(new TournamentTeam
            {
                TournamentId = tournament.Id,
                TeamId = team.Id
            });
        }
        await context.SaveChangesAsync();

        // ═══ 6. PARTIDOS (Todos contra todos: 19 fechas, 10 partidos por fecha = 190) ═══
        var matchDate = tournament.StartDate; // 16 de enero 2026
        var teamList = teams.ToList();
        int totalTeams = teamList.Count;

        // Round-Robin: fijar el primer equipo y rotar los demás
        var rotating = teamList.Skip(1).ToList(); // 19 equipos rotan

        for (int round = 0; round < totalTeams - 1; round++)
        {
            int matchDay = round + 1; // Fechas del 1 al 19

            for (int match = 0; match < totalTeams / 2; match++)
            {
                // Emparejar desde los extremos
                Team home, away;
                if (match == 0)
                {
                    home = teamList[0];
                    away = rotating[0];
                }
                else
                {
                    home = rotating[match];
                    away = rotating[rotating.Count - match];
                }

                // Alternar localía en fechas pares
                if (round % 2 == 1)
                    (home, away) = (away, home);

                context.Matches.Add(new Match
                {
                    TournamentId = tournament.Id,
                    HomeTeamId = home.Id,
                    AwayTeamId = away.Id,
                    RefereeId = referees[match % referees.Count].Id,
                    Matchday = matchDay, // Fecha 1 a 19
                    MatchDate = matchDate.AddHours(match < 5 ? 16 : 19),
                    Venue = home.Stadium,
                    Status = MatchStatus.Scheduled
                });
            }

            // Rotar: mover el primero al final
            var first = rotating[0];
            rotating.RemoveAt(0);
            rotating.Add(first);

            // Siguiente fecha: 7 días después
            matchDate = matchDate.AddDays(7);
        }

        await context.SaveChangesAsync();

    }
}


