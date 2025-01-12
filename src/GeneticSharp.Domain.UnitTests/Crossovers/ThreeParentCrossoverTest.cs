﻿using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

namespace GeneticSharp.Domain.UnitTests.Crossovers
{
    [TestFixture]
    [Category("Crossovers")]
    public class ThreeParentCrossoverTest
    {
        [TearDown]
        public void Cleanup()
        {
            RandomizationProvider.Current = new BasicRandomization();
        }

        [Test]
        public void Cross_ThreeParents_OneChildren()
        {
            var chromosome1 = Substitute.For<ChromosomeBase>(4);
            chromosome1.ReplaceGenes(0, new Gene[]
            {
                new Gene(1),
                new Gene(2),
                new Gene(3),
                new Gene(4),
            });
            chromosome1.CreateNew().Returns(Substitute.For<ChromosomeBase>(4));

            var chromosome2 = Substitute.For<ChromosomeBase>(4);
            chromosome2.ReplaceGenes(0, new Gene[]
            {
                new Gene(1),
                new Gene(5),
                new Gene(6),
                new Gene(4)
            });
            chromosome2.CreateNew().Returns(Substitute.For<ChromosomeBase>(4));

            var chromosome3 = Substitute.For<ChromosomeBase>(4);
            chromosome3.ReplaceGenes(0, new Gene[]
            {
                new Gene(10),
                new Gene(11),
                new Gene(12),
                new Gene(13)
            });
            chromosome3.CreateNew().Returns(Substitute.For<ChromosomeBase>(4));

            var parents = new List<IChromosome>() { chromosome1, chromosome2, chromosome3 };

            var target = new ThreeParentCrossover();

            var actual = target.Cross(parents);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(4, actual[0].Length);

            Assert.AreEqual(1, actual[0].GetGene(0).Value);
            Assert.AreEqual(11, actual[0].GetGene(1).Value);
            Assert.AreEqual(12, actual[0].GetGene(2).Value);
            Assert.AreEqual(4, actual[0].GetGene(3).Value);
        }
    }
}