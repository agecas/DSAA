using DSAA.Tree.Avl;
using DSAA.Tree.UnitTests.Helpers;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Tree.UnitTests.Avl
{
    /// <summary>
    /// https://stackoverflow.com/questions/3955680/how-to-check-if-my-avl-tree-implementation-is-correct
    /// </summary>
    
    public sealed class ImmutableAvlTreeTests
    {
        private ImmutableAvlTree<int, string> CreateSut() => new ImmutableAvlTree<int, string>(new IntComparer());

        #region Insert

        [Fact]
        public void Given_EmptyTree_When_ValueInserted_Then_ValueBecomesRoot()
        {
            // Arrange
            var sut = CreateSut()
                .Add(1, "A");

            // Act
            // Assert
            sut.AssertRootEqual(1, "A");
        }

        [Fact]
        public void Given_Tree_When_ValueInsertedIsSmallerThanRoot_Then_ValueIsInsertedAsLeftNode()
        {
            // Arrange
            // Act
            var sut = CreateSut()
                .Add(2, "B")
                .Add(1, "A");

            // Assert
            //   B
            //  /
            // A
            sut.Root.Left.AssertEqual(1, "A");
        }

        [Fact]
        public void Given_Tree_When_ValueInsertedIsLargerThanRoot_Then_ValueIsInsertedAsRightNode()
        {
            // Arrange
            // Act
            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C");

            // Assert
            // B
            //  \
            //   C
            sut.Root.Right.AssertEqual(3, "C");
        }

        [Fact]
        public void Given_Tree_When_ValueForExistingKeyInserted_Then_AddValueUnderThatKey()
        {
            // Arrange

            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C")
                .Add(1, "A");

            // Act
            sut = sut.Add(3, "2C")
                     .Add(1, "2A");

            // Assert
            //   B
            //  / \
            // A   C

            Assert.Collection(sut.Root.Left.Values, v =>
            {
                Assert.Equal("A", v);
            }, v =>
            {
                Assert.Equal("2A", v);
            });

            Assert.Collection(sut.Root.Right.Values, v =>
            {
                Assert.Equal("C", v);
            }, v =>
            {
                Assert.Equal("2C", v);
            });
        }

        [Fact]
        public void Given_Tree_When_ValueForRootKeyInserted_Then_AddValueUnderThatKey()
        {
            // Arrange
            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C")
                .Add(1, "A");

            // Act
            sut = sut.Add(2, "2B");

            // Assert
            Assert.Collection(sut.Root.Values, v =>
            {
                Assert.Equal("B", v);
            }, v =>
            {
                Assert.Equal("2B", v);
            });
        }

        [Fact]
        public void Given_Tree_When_InsertedValuesResultsInRightHeavyTree_Then_LeftRotationShouldBePerformed()
        {
            // Arrange
            //     C
            //    /
            //   B
            //  /
            // A
            var sut = CreateSut()
                .Add(3, "C")
                .Add(2, "B")
                .Add(1, "A");

            // Act
            // Assert
            //   B
            //  / \
            // A   C
            sut.AssertRootEqual(2, "B");
            sut.Root.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertLeafEqual(3, "C");
        }

        [Fact]
        public void Given_Tree_When_InsertedValuesResultsInLeftHeavyTree_Then_RightRotationShouldBePerformed()
        {
            // Arrange
            // A
            //  \
            //   B
            //    \
            //     C
            var sut = CreateSut()
                .Add(1, "A")
                .Add(2, "B")
                .Add(3, "C");

            // Act
            // Assert
            //   B
            //  / \
            // A   C
            sut.AssertRootEqual(2, "B");
            sut.Root.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertLeafEqual(3, "C");
        }

        [Fact]
        public void Given_Tree_When_InsertedValuesResultsInRightHeavyTree_Then_LeftRightRotationShouldBePerformed()
        {
            // Arrange
            // A
            //  \
            //   C
            //  /
            // B

            var sut = CreateSut()
                .Add(1, "A")
                .Add(3, "C")
                .Add(2, "B");

            // Act
            // Assert
            //   B
            //  / \
            // A   C
            sut.AssertRootEqual(2, "B");
            sut.Root.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertLeafEqual(3, "C");
        }

        [Fact]
        public void Given_Tree_When_InsertedValuesResultsInLeftHeavyTree_Then_RightLeftRotationShouldBePerformed()
        {
            // Arrange
            //     C
            //    /
            //   A
            //    \
            //     B

            var sut = CreateSut()
                .Add(3, "C")
                .Add(1, "A")
                .Add(2, "B");

            // Act
            // Assert
            //   B
            //  / \
            // A   C
            sut.AssertRootEqual(2, "B");
            sut.Root.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertLeafEqual(3, "C");
        }

        [Fact]
        public void Given_Tree_When_InsertedValuesResultsInVariousInBalances_Then_BalanceTree()
        {
            // Arrange
            //   B
            //  / \
            // A   C
            //      \
            //       D
            //        \
            //         E
            //          \
            //           F

            var sut = CreateSut()
                .Add(3, "C")
                .Add(2, "B")
                .Add(1, "A")
                .Add(4, "D")
                .Add(5, "E")
                .Add(6, "F");

            // Act
            // Assert
            //     D
            //    / \
            //   B   E
            //  / \   \
            // A   C   F

            sut.AssertRootEqual(4, "D");
            sut.Root.Left.AssertEqual(2, "B");
            sut.Root.Left.Left.AssertEqual(1, "A");
            sut.Root.Left.Right.AssertEqual(3, "C");
            sut.Root.Right.AssertEqual(5, "E");
            sut.Root.Right.Right.AssertEqual(6, "F");
        }

        [Fact]
        public void Given_ComplexTree_When_TreeIsRight_Then_PerformRightRotation()
        {
            // Arrange
            //      C 
            //     / \
            //    B   E
            //       / \
            //      D   F
            //           \
            //            G

            var sut = CreateSut()
                .Add(3, "C")
                .Add(2, "B")
                .Add(5, "E")
                .Add(4, "D")
                .Add(6, "F")
                .Add(7, "G");

            // Act
            // Assert
            //      E 
            //     / \
            //    C   F
            //   / \   \
            //  B   D   G

            sut.AssertRootEqual(5, "E");
            sut.Root.Left.AssertBranchEqual(3, "C");
            sut.Root.Left.Left.AssertLeafEqual(2, "B");
            sut.Root.Left.Right.AssertLeafEqual(4, "D");
            sut.Root.Right.AssertBranchEqual(6, "F");
            sut.Root.Right.Right.AssertLeafEqual(7, "G");
        }

        [Fact]
        public void Given_ComplexTree_When_TreeIsLeftHeavy_Then_PerformLeftRotation()
        {
            // Arrange
            //        E 
            //      /   \
            //     C     F
            //    / \ 
            //   B   D
            //  /
            // A

            var sut = CreateSut()
                .Add(5, "E")
                .Add(3, "C")
                .Add(6, "F")
                .Add(2, "B")
                .Add(4, "D")
                .Add(1, "A");

            // Act
            // Assert
            //      C 
            //     / \
            //    B   E
            //   /   / \
            //  A   D   F

            sut.AssertRootEqual(3, "C");
            sut.Root.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertBranchEqual(5, "E");
            sut.Root.Right.Left.AssertLeafEqual(4, "D");
            sut.Root.Right.Right.AssertLeafEqual(6, "F");
        }

        [Fact]
        public void Given_ComplexTree_When_RightSubtreeHeavy_Then_PerformRightLeftRotation()
        {
            // Arrange
            //         E 
            //      /     \
            //     C       J
            //    / \     / \
            //   A   D   H   K
            //          / \   \
            //         G   I   L
            //        /
            //       F

            var sut = CreateSut()
                .Add(5, "E")
                .Add(3, "C")
                .Add(10, "J")
                .Add(1, "A")
                .Add(4, "D")
                .Add(8, "H")
                .Add(11, "K")
                .Add(7, "G")
                .Add(9, "I")
                .Add(12, "L")
                .Add(6, "F");

            // Act
            // Assert
            //          H 
            //       /      \
            //      E        J
            //    /   \     / \
            //   C     G   I   K
            //  / \   /         \
            // A   D F           L

            sut.AssertRootEqual(8, "H");
            sut.Root.Left.AssertBranchEqual(5, "E");
            sut.Root.Left.Left.AssertBranchEqual(3, "C");
            sut.Root.Left.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Left.Right.AssertLeafEqual(4, "D");
            sut.Root.Left.Right.AssertBranchEqual(7, "G");
            sut.Root.Left.Right.Left.AssertLeafEqual(6, "F");
            sut.Root.Right.AssertBranchEqual(10, "J");
            sut.Root.Right.Left.AssertLeafEqual(9, "I");
            sut.Root.Right.Right.AssertBranchEqual(11, "K");
            sut.Root.Right.Right.Right.AssertLeafEqual(12, "L");
        }

        [Fact]
        public void Given_ComplexTree_When_LeftSubtreeHeavy_Then_PerformLeftRightRotation()
        {
            // Arrange
            //         H 
            //      /     \
            //     C       K
            //    / \     / \
            //   B   E   I   L
            //  /   / \
            // A   D   F
            //          \
            //           G

            var sut = CreateSut()
                .Add(8, "H")
                .Add(3, "C")
                .Add(11, "K")
                .Add(2, "B")
                .Add(5, "E")
                .Add(9, "I")
                .Add(12, "L")
                .Add(1, "A")
                .Add(4, "D")
                .Add(6, "F")
                .Add(7, "G");

            // Act
            // Assert
            //         E 
            //      /     \
            //     C        H
            //    / \     /   \
            //   B   D   F     K
            //  /         \   / \
            // A           G I   L

            sut.AssertRootEqual(5, "E");
            sut.Root.Left.AssertBranchEqual(3, "C");
            sut.Root.Left.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Right.AssertLeafEqual(4, "D");
            sut.Root.Right.AssertBranchEqual(8, "H");
            sut.Root.Right.Left.AssertBranchEqual(6, "F");
            sut.Root.Right.Left.Right.AssertLeafEqual(7, "G");
            sut.Root.Right.Right.AssertBranchEqual(11, "K");
            sut.Root.Right.Right.Left.AssertLeafEqual(9, "I");
            sut.Root.Right.Right.Right.AssertLeafEqual(12, "L");
        }

        #endregion

        #region Delete

        [Fact]
        public void Given_EmptyTree_When_DeleteRequested_Then_Return()
        {
            // Arrange
            // Act
            var sut = CreateSut()
                .Delete(1);

            // Assert
            Assert.True(sut.IsEmpty);
        }

        [Fact]
        public void Given_Tree_When_DeleteForNonExistingKeyRequested_Then_Return()
        {
            // Arrange
            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C")
                .Add(1, "A");

            // Act
            sut = sut.Delete(4);

            // Assert
            sut.AssertRootEqual(2, "B");
            sut.Root.Right.AssertLeafEqual(3, "C");
            sut.Root.Left.AssertLeafEqual(1, "A");
        }

        [Fact]
        public void Given_TreeWithRootNode_When_DeleteRequested_Then_ReturnEmpty()
        {
            // Arrange
            var sut = CreateSut()
                .Add(2, "B");

            // Act
            sut = sut.Delete(2);

            // Assert
            Assert.True(sut.IsEmpty);
        }

        [Fact]
        public void Given_Tree_When_DeletingRootNode_Then_PromoteHighestValueFromRightSide()
        {
            // Arrange
            //       B
            //     /   \
            //    A     C

            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C")
                .Add(1, "A");

            // Act
            sut = sut.Delete(2);

            // Assert
            //    C
            //  /
            // A

            sut.AssertRootEqual(3, "C");
            Assert.Null(sut.Root.Right);
            sut.Root.Left.AssertLeafEqual(1, "A");
        }

        [Fact]
        public void Given_Tree_When_DeletingLeafNode_Then_ReturnTreeWithNoRightNode()
        {
            // Arrange
            //       B
            //     /   \
            //    A     C

            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C")
                .Add(1, "A");

            // Act
            sut = sut.Delete(3);

            // Assert
            //      B
            //     /
            //    A

            sut.AssertRootEqual(2, "B");
            Assert.Null(sut.Root.Right);
            sut.Root.Left.AssertLeafEqual(1, "A");
        }

        [Fact]
        public void Given_Tree_When_DeletingLeafNode_Then_ReturnTreeWithNoLeftNode()
        {
            // Arrange
            //       B
            //     /   \
            //    A     C

            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C")
                .Add(1, "A");

            // Act
            sut = sut.Delete(1);

            // Assert
            //       B
            //        \
            //         C

            sut.AssertRootEqual(2, "B");
            sut.Root.Right.AssertEqual(3, "C");
            Assert.Null(sut.Root.Left);
        }

        [Fact]
        public void Given_Tree_When_DeletingLeafNodeFromLeftSideOfRoot_Then_PerformLeftRotation()
        {
            // Arrange
            //       B
            //     /   \
            //    A     C
            //           \
            //            D

            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C")
                .Add(1, "A")
                .Add(4, "D");

            // Act
            sut = sut.Delete(1);

            // Assert
            //       C
            //      / \
            //     B   D

            sut.AssertRootEqual(3, "C");
            sut.Root.Left.AssertLeafEqual(2, "B");
            sut.Root.Right.AssertLeafEqual(4, "D");
        }

        [Fact]
        public void Given_Tree_When_DeletingLeafNodeFromRightSideOfRoot_Then_PerformRightRotation()
        {
            // Arrange
            //       C
            //     /   \
            //    B     D
            //   /
            //  A

            var sut = CreateSut()
                .Add(2, "B")
                .Add(3, "C")
                .Add(1, "A")
                .Add(4, "D");

            // Act
            sut = sut.Delete(4);

            // Assert
            //       B
            //      / \
            //     A   C

            sut.AssertRootEqual(2, "B");
            sut.Root.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertLeafEqual(3, "C");
        }

        [Fact]
        public void Given_Tree_When_DeletingLeafNodeFromLeftSideOfRoot_Then_PerformRightLeftRotation()
        {
            // Arrange
            //    B 
            //   / \
            //  A   D
            //     /
            //    C 

            var sut = CreateSut()
                .Add(2, "B")
                .Add(4, "D")
                .Add(1, "A")
                .Add(3, "C");

            // Act
            sut = sut.Delete(1);

            // Assert
            //       C
            //      / \
            //     B   D

            sut.AssertRootEqual(3, "C");
            sut.Root.Left.AssertLeafEqual(2, "B");
            sut.Root.Right.AssertLeafEqual(4, "D");
        }

        [Fact]
        public void Given_Tree_When_DeletingLeafNodeFromRightSideOfRoot_Then_PerformLeftRightRotation()
        {
            // Arrange
            //    C 
            //   / \
            //  A   D
            //   \  
            //    B 

            var sut = CreateSut()
                .Add(3, "C")
                .Add(1, "A")
                .Add(4, "D")
                .Add(2, "B");

            // Act
            sut = sut.Delete(4);

            // Assert
            //       B
            //      / \
            //     A   C

            sut.AssertRootEqual(2, "B");
            sut.Root.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertLeafEqual(3, "C");
        }

        [Fact]
        public void Given_ComplexTree_When_DeletingLeafNodeFromRightSideOfRoot_Then_PerformRightRotation()
        {
            // Arrange
            //      C 
            //     / \
            //    B   E
            //   /   / \
            //  A   D   F
            //           \
            //            G

            var sut = CreateSut()
                .Add(3, "C")
                .Add(2, "B")
                .Add(5, "E")
                .Add(4, "D")
                .Add(6, "F")
                .Add(1, "A")
                .Add(7, "G");

            // Act
            sut = sut.Delete(1);

            // Assert
            //      E 
            //     / \
            //    C   F
            //   / \   \
            //  B   D   G

            sut.AssertRootEqual(5, "E");
            sut.Root.Left.AssertBranchEqual(3, "C");
            sut.Root.Left.Left.AssertLeafEqual(2, "B");
            sut.Root.Left.Right.AssertLeafEqual(4, "D");
            sut.Root.Right.AssertBranchEqual(6, "F");
            sut.Root.Right.Right.AssertLeafEqual(7, "G");
        }

        [Fact]
        public void Given_ComplexTree_When_DeletingLeafNodeFromLeftSideOfRoot_Then_PerformLeftRotation()
        {
            // Arrange
            //        E 
            //      /   \
            //     C     G
            //    / \   /
            //   B   D F
            //  /
            // A

            var sut = CreateSut()
               .Add(5, "E")
               .Add(3, "C")
               .Add(7, "G")
               .Add(2, "B")
               .Add(4, "D")
               .Add(6, "F")
               .Add(1, "A");


            // Act
            sut = sut.Delete(7);

            // Assert
            //      C 
            //     / \
            //    B   E
            //   /   / \
            //  A   D   F

            sut.AssertRootEqual(3, "C");
            sut.Root.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertBranchEqual(5, "E");
            sut.Root.Right.Left.AssertLeafEqual(4, "D");
            sut.Root.Right.Right.AssertLeafEqual(6, "F");
        }

        [Fact]
        public void Given_ComplexTree_When_DeletingLeafNodeFromLeftSideOfRoot_Then_PerformRightLeftRotation()
        {
            // Arrange
            //         E 
            //      /     \
            //     C       J
            //    / \     / \
            //   A   D   H   K
            //    \     / \   \
            //     B   G   I   L
            //        /
            //       F

            var sut = CreateSut()
                .Add(5, "E")
                .Add(3, "C")
                .Add(10, "J")
                .Add(1, "A")
                .Add(4, "D")
                .Add(8, "H")
                .Add(11, "K")
                .Add(7, "G")
                .Add(9, "I")
                .Add(12, "L")
                .Add(2, "B")
                .Add(6, "F");

            // Act
            sut = sut.Delete(2);

            // Assert
            //          H 
            //       /      \
            //      E        J
            //    /   \     / \
            //   C     G   I   K
            //  / \   /         \
            // A   D F           L

            sut.AssertRootEqual(8, "H");
            sut.Root.Left.AssertBranchEqual(5, "E");
            sut.Root.Left.Left.AssertBranchEqual(3, "C");
            sut.Root.Left.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Left.Right.AssertLeafEqual(4, "D");
            sut.Root.Left.Right.AssertBranchEqual(7, "G");
            sut.Root.Left.Right.Left.AssertLeafEqual(6, "F");
            sut.Root.Right.AssertBranchEqual(10, "J");
            sut.Root.Right.Left.AssertLeafEqual(9, "I");
            sut.Root.Right.Right.AssertBranchEqual(11, "K");
            sut.Root.Right.Right.Right.AssertLeafEqual(12, "L");
        }

        [Fact] 
        public void Given_ComplexTree_When_DeletingLeafNodeFromRightSideOfRoot_Then_PerformLeftRightRotation()
        {
            // Arrange
            //         H 
            //      /     \
            //     C       K
            //    / \     / \
            //   B   E   I   L
            //  /   / \   \
            // A   D   F   J
            //          \
            //           G

            var sut = CreateSut()
                .Add(8, "H")
                .Add(3, "C")
                .Add(11, "K")
                .Add(2, "B")
                .Add(5, "E")
                .Add(9, "I")
                .Add(12, "L")
                .Add(1, "A")
                .Add(4, "D")
                .Add(6, "F")
                .Add(10, "J")
                .Add(7, "G");

            // Act
            sut = sut.Delete(10);

            // Assert
            //         E 
            //      /     \
            //     C        H
            //    / \     /   \
            //   B   D   F     K
            //  /         \   / \
            // A           G I   L

            sut.AssertRootEqual(5, "E");
            sut.Root.Left.AssertBranchEqual(3, "C");
            sut.Root.Left.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Right.AssertLeafEqual(4, "D");
            sut.Root.Right.AssertBranchEqual(8, "H");
            sut.Root.Right.Left.AssertBranchEqual(6, "F");
            sut.Root.Right.Left.Right.AssertLeafEqual(7, "G");
            sut.Root.Right.Right.AssertBranchEqual(11, "K");
            sut.Root.Right.Right.Left.AssertLeafEqual(9, "I");
            sut.Root.Right.Right.Right.AssertLeafEqual(12, "L");
        }

        #endregion

        #region Find

        [Theory]
        [InlineData(3, "C")]
        [InlineData(8, "H")]
        [InlineData(5, "E")]
        public void Given_Tree_When_SearchingForKey_And_KeyFound_Then_ReturnValue(int key, string value)
        {
            // Arrange
            //       D
            //     /   \
            //    B     F
            //   / \   / \
            //  A   C E   G
            //             \
            //              H

            var sut = CreateSut()
                .Add(4, "D")
                .Add(2, "B")
                .Add(1, "A")
                .Add(3, "C")
                .Add(6, "F")
                .Add(5, "E")
                .Add(7, "G")
                .Add(8, "H");

            // Act
            var result = sut.Find(key);

            // Assert
            Assert.Equal(new [] { value }, result);
        } 
        
        [Fact]
        public void Given_Tree_When_SearchingForKey_And_KeyWithMultipleValuesFound_Then_ReturnValues()
        {
            // Arrange
            //       D
            //     /   \
            //    B     F
            //   / \   / \
            //  A   C E   G
            //             \
            //              H

            var sut = CreateSut()
                .Add(4, "D")
                .Add(2, "B")
                .Add(1, "A")
                .Add(3, "C")
                .Add(6, "F")
                .Add(5, "E")
                .Add(7, "G1")
                .Add(7, "G2")
                .Add(8, "H");

            // Act
            var result = sut.Find(7);

            // Assert
            Assert.Equal(new [] { "G1", "G2" }, result);
        }  
        
        [Fact]
        public void Given_Tree_When_SearchingForKey_And_KeyNotFound_Then_ReturnEmpty()
        {
            // Arrange
            var sut = CreateSut();

            //       D
            //     /   \
            //    B     F
            //   / \   / \
            //  A   C E   G
            //             \
            //              H

            sut.Add(4, "D");
            sut.Add(2, "B");
            sut.Add(1, "A");
            sut.Add(3, "C");
            sut.Add(6, "F");
            sut.Add(5, "E");
            sut.Add(7, "G1");
            sut.Add(7, "G2");
            sut.Add(8, "H");

            // Act
            var result = sut.Find(9);

            // Assert
            Assert.Empty(result);
        }

        #endregion
    }
}
