using BedrockTools.Objects;
using BedrockTools.Objects.Blocks;
using BedrockTools.Structure.Features.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace BedrockTools.Structure.Features.Nature.TreeA
{
    public class Tree : DynamicFeature {
        Block woodBlock;
        Block leafBlock;

        Dimensions range;

        List<Branch> branches;
        List<Branch> leaves;

        TreeParams config;

        public Tree(WoodBlock woodBlock, Block leafBlock, Dimensions halfRenderRange, TreeParams config) {
            this.woodBlock = woodBlock;
            this.leafBlock= leafBlock;
            this.config = config;
            range = halfRenderRange; 
            branches = new List<Branch>();
            leaves = new List<Branch>();
            BuildCache();
        }

        void BuildRoot() {
            branches.Add(
                new Branch(
                    Vector3.Zero,
                    Vector3.UnitY * config.rootHeight,
                    config.rootWidth,
                    config.trunkWidth,
                    3f
                )
            );
        }


        void BuildTrunk() {

            branches.AddRange(
                new SuperBranch() {
                    pointA = Vector3.UnitY * config.rootHeight,
                    pointB = Vector3.UnitY * config.rootHeight + Vector3.UnitY * config.trunkHeight,
                    widthA = config.trunkWidth,
                    widthB = config.trunkWidth,
                    segments = config.trunkSegments,
                    deviation = config.trunkDeviation,
                    tolerance = 6f
                });
        }
        void AddLeafFromBranch( Branch cur ) {
            for (int i = 0; i < config.leaveCount; i++) {
                Vector3 v = Vector3.Normalize(cur.pointB - cur.pointA) * config.leaveLenght;
                Branch leaf = new Branch(cur.pointB, cur.pointB+v, config.leaveWidth, config.leaveWidth/2, 0f);
                leaf.CreekBranch(0, config.leaveAngle);
                leaves.Add(leaf);
            }
        }
        void BuildCanopy() {
            Vector3 pointA = Vector3.UnitY * (config.rootHeight + config.trunkHeight);
            Random rand = new Random();
            List<Branch> futureBranches;
            List<Branch> currentBranches = new List<Branch>();
            for (int branches = 0; branches< config.branches; branches++) {
                float theta = 2f*MathF.PI*branches / config.branches;
                float x = MathF.Cos(theta);
                float z = MathF.Sin(theta);
                float y = MathF.Cos((float)rand.NextDouble()*MathF.PI/2f);
                Vector3 v = new Vector3(x,y,z);
                v = Vector3.Normalize(v) * config.branchLength;
                Branch branch = new Branch(pointA, v+pointA, config.branchWidth, config.branchWidth, 2f);
                branch.CreekBranch(0f, config.branchTwist);
                currentBranches.Add(branch);
            }

            while (currentBranches.Count > 0) {
                futureBranches = new List<Branch>();
                foreach(Branch cur in currentBranches) {
                    branches.Add(cur);
                    if (rand.NextDouble() > config.branchExtendChance) {
                        AddLeafFromBranch(cur);
                        continue;
                    }
                    float nextWidht = cur.widthB * ((float)rand.NextDouble()*(config.branchMaxWidthDecay-config.branchMinWidthDecay)+config.branchMinWidthDecay);
                    if (nextWidht < 1f) {
                        AddLeafFromBranch(cur);
                        continue;
                    }
                    Branch nBranch = new Branch(
                       cur.pointB,
                       2f * cur.pointB - cur.pointA,
                       cur.widthB,
                       nextWidht,
                       2f
                    );
                    nBranch.CreekBranch(0f, config.branchTwist);
                    futureBranches.Add(nBranch);
                    if (rand.NextDouble() < config.splitProbability) {
                        nBranch = new Branch(
                           cur.pointB,
                           2f * cur.pointB - cur.pointA,
                           cur.widthB,
                           nextWidht,
                           2f
                        );
                        nBranch.CreekBranch(config.splitMinAngle, config.splitMaxAngle);
                        futureBranches.Add(nBranch);
                    }
                }
                currentBranches = futureBranches;
            }
        }
        protected override void CalculateFeature() {
            BuildRoot();
            BuildTrunk();
            BuildCanopy();


            foreach (Branch branch in branches) {
                Vector3 mi = Vector3.Min(branch.pointA, branch.pointB) - Vector3.One * (branch.toleranceEnd + float.Max(branch.widthA, branch.widthB));
                Vector3 mx = Vector3.Max(branch.pointA, branch.pointB) + Vector3.One * (branch.toleranceEnd + float.Max(branch.widthA, branch.widthB));
                for (int x = (int)Math.Floor(mi.X); x <= (int)Math.Ceiling(mx.X); x++) {
                    for (int y = (int)Math.Floor(mi.Y); y <= (int)Math.Ceiling(mx.Y); y++) {
                        for (int z = (int)Math.Floor(mi.Z); z <= (int)Math.Ceiling(mx.Z); z++) {
                            IntCoords c = new IntCoords(x, y, z);
                            if (Cache.ContainsKey(c)) continue;
                            if (branch.TestPoint(new Vector3(x, y, z))) {
                                Cache.Add(c, woodBlock);
                            }
                        }
                    }
                }
            }

            foreach (Branch branch in leaves) {
                Vector3 mi = Vector3.Min(branch.pointA, branch.pointB) - Vector3.One * (branch.toleranceEnd + float.Max(branch.widthA, branch.widthB));
                Vector3 mx = Vector3.Max(branch.pointA, branch.pointB) + Vector3.One * (branch.toleranceEnd + float.Max(branch.widthA, branch.widthB));
                for (int x = (int)Math.Floor(mi.X); x <= (int)Math.Ceiling(mx.X); x++) {
                    for (int y = (int)Math.Floor(mi.Y); y <= (int)Math.Ceiling(mx.Y); y++) {
                        for (int z = (int)Math.Floor(mi.Z); z <= (int)Math.Ceiling(mx.Z); z++) {
                            IntCoords c = new IntCoords(x, y, z);
                            if (Cache.ContainsKey(c)) continue;
                            if (branch.TestPoint(new Vector3(x, y, z))) {
                                Cache.Add(c, leafBlock);
                            }
                        }
                    }
                }
            }

        }

        public class TreeParams {

            public float rootWidth;
            public float rootHeight;
            public float rootSegmentHeight;

            public float trunkWidth;
            public float trunkHeight;
            public int trunkSegments;
            public float trunkDeviation;

            public int branches;


            public float branchTwist;
            public float branchLength;

            public float branchWidth;
            public float branchMinWidthDecay;
            public float branchMaxWidthDecay;

            public float branchExtendChance;

            public float splitProbability;
            public float splitMinAngle;
            public float splitMaxAngle;

            public float leaveCount;
            public float leaveWidth;
            public float leaveLenght;
            public float leaveAngle;
        }

    }
}
