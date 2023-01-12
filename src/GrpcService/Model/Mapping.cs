using GrpcService.Protos;

namespace GrpcService.Model
{
    public static class Mapping
    {
        public static SuperHeroResponseProto ToSuperHeroResponseProto(this SuperHeroResponse response)
        {
            var protoResponse = new SuperHeroResponseProto()
            {
                Id = response.Id,
                Name = response.Name,
                Slug = response.Slug,
                Powerstats = new()
                {
                    Combat = response.Powerstats.Combat,
                    Durability = response.Powerstats.Durability,
                    Intelligence = response.Powerstats.Intelligence,
                    Power = response.Powerstats.Power,
                    Speed = response.Powerstats.Speed,
                    Strength = response.Powerstats.Strength,
                },
                Appearance = new()
                {
                    EyeColor = response.Appearance.EyeColor,
                    Gender = response.Appearance.Gender,
                    HairColor = response.Appearance.HairColor,
                    Race = response.Appearance.Race,
                },
                Biography = new()
                {
                    Alignment = response.Biography.Alignment,
                    AlterEgos = response.Biography.AlterEgos,
                    FirstAppearance = response.Biography.FirstAppearance,
                    FullName = response.Biography.FullName,
                    PlaceOfBirth = response.Biography.PlaceOfBirth,
                    Publisher = response.Biography.Publisher,
                },
                Connections = new()
                {
                    GroupAffiliation = response.Connections.GroupAffiliation,
                    Relatives = response.Connections.Relatives,
                },
                Images = new()
                {
                    Lg = response.Images.Lg.OriginalString,
                    Md = response.Images.Md.OriginalString,
                    Sm = response.Images.Sm.OriginalString,
                    Xs = response.Images.Xs.OriginalString,
                },
                Work = new()
                {
                    Base = response.Work.Base,
                    Occupation = response.Work.Occupation,
                }
            };

            protoResponse.Appearance.Height.Add(response.Appearance.Height);
            protoResponse.Appearance.Weight.Add(response.Appearance.Weight);

            protoResponse.Biography.Aliases.Add(response.Biography.Aliases);

            return protoResponse;
        }
    }
}
