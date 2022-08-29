﻿using ProjectMateTaskDalTests.EnitiesTests.Base;
using ProjectMateTaskDalTests.Resources;
using ProjetMateTaskEntities.Entities.Actors;
using ProjetMateTaskEntities.Entities.Base;
using ProjetMateTaskEntities.Entities.Types;

namespace ProjectMateTaskDalTests.EnitiesTests;

[TestClass]
public class ClientTests : NamedEntityTests<Client>
{
    public override void SpecifiedCheckInHaveErrors(Client namedEntity)
    {
        namedEntity.Name = string.Concat(Enumerable.Repeat("S" , 151));
        
        Assert.IsTrue(namedEntity.HasErrors);

        namedEntity.Name = "SomeNamedEntity";
        
        Assert.IsTrue(namedEntity.HasErrors);
        
        namedEntity.Manager = GlobalResources.GetRandomEntity<Manager>();
        
        Assert.IsTrue(namedEntity.HasErrors);

        namedEntity.Status = GlobalResources.GetRandomEntity<ClientStatus>();
        
        Assert.IsFalse(namedEntity.HasErrors);
    }
}