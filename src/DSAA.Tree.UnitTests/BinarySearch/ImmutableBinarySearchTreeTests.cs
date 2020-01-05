using DSAA.Tree.BinarySearch;
using DSAA.Tree.UnitTests.Helpers;
using DSAA.UnitTests.Shared;
using Xunit;

namespace DSAA.Tree.UnitTests.BinarySearch
{
    public sealed class ImmutableBinarySearchTreeTests
    {
        private ImmutableBinarySearchTree<int, string> CreateSut() => new ImmutableBinarySearchTree<int, string>(new IntComparer());

        #region Insert

        [Fact]
        public void Given_EmptyTree_When_ValueInserted_Then_ValueBecomesRoot()
        {
            // Arrange
            // Act
            var sut = CreateSut()
                .Add(1, "A");

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
            sut.AssertRootEqual(2, "B");
            sut.Root.Left.AssertLeafEqual(1, "A");
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
            sut.AssertRootEqual(2, "B");
            sut.Root.Right.AssertLeafEqual(3, "C");
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
            sut = sut
                .Add(3, "2C")
                .Add(1, "2A");

            // Assert
            sut.AssertRootEqual(2, "B");
            sut.Root.Right.AssertLeafEqual(3, "C", "2C");
            sut.Root.Left.AssertLeafEqual(1, "A", "2A");
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
            sut.AssertRootEqual(2, "B", "2B");
        }

        [Fact]
        public void Given_Tree_When_MultipleValuesInserted_Then_CorrectTreeShouldBeProduced()
        {
            // Arrange
            // Act
            var sut = CreateSut()
                .Add(5, "E")
                .Add(3, "C")
                .Add(7, "G")
                .Add(4, "D")
                .Add(10, "J")
                .Add(9, "I")
                .Add(1, "A")
                .Add(6, "F")
                .Add(2, "B")
                .Add(8, "H");

            // Assert
            //       E
            //     /   \
            //    C      G
            //   / \    / \
            //  A   D  F   J
            //   \        /
            //    B      I
            //          /
            //         H

            sut.AssertRootEqual(5, "E");
            sut.Root.Left.AssertBranchEqual(3, "C");
            sut.Root.Left.Left.AssertBranchEqual(1, "A");
            sut.Root.Left.Left.Right.AssertLeafEqual(2, "B");
            sut.Root.Left.Right.AssertLeafEqual(4, "D");
            sut.Root.Right.AssertBranchEqual(7, "G");
            sut.Root.Right.Left.AssertLeafEqual(6, "F");
            sut.Root.Right.Right.AssertBranchEqual(10, "J");
            sut.Root.Right.Right.Left.AssertBranchEqual(9, "I");
            sut.Root.Right.Right.Left.Left.AssertLeafEqual(8, "H");
        }

        #endregion

        #region Delete

        [Fact]
        public void Given_EmptyTree_When_DeleteRequested_Then_Return()
        {
            // Arrange
            // Act
            var sut = CreateSut().Delete(1);

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
        public void Given_Tree_When_DeletingRootNode_Then_PromoteHighestValueFromLeftSide()
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
            sut.Root.Right.AssertLeafEqual(3, "C");
            Assert.Null(sut.Root.Left);
        }

        [Fact]
        public void Given_Tree_When_DeletingNodeWithNoRightChild_From_RightOfRoot_Then_PromoteLeftChildToRemovedNodesPlace()
        {
            // Arrange
            //       D
            //     /   \
            //    B     H
            //   / \   /
            //  A   C F
            //       / \
            //      E   G

            var sut = CreateSut()
                        .Add(4, "D")
                        .Add(2, "B")
                        .Add(1, "A")
                        .Add(3, "C")
                        .Add(8, "H")
                        .Add(6, "F")
                        .Add(5, "E")
                        .Add(7, "G");

            // Act
            sut = sut.Delete(8);

            // Assert
            //       D
            //     /   \
            //    B     F
            //   / \   / \
            //  A   C E   G

            sut.AssertRootEqual(4, "D");
            sut.Root.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Right.AssertLeafEqual(3, "C");
            sut.Root.Right.AssertBranchEqual(6, "F");
            sut.Root.Right.Left.AssertLeafEqual(5, "E");
            sut.Root.Right.Right.AssertLeafEqual(7, "G");
        }

        [Fact]
        public void Given_Tree_When_DeletingNodeWithNoRightChild_From_LeftOfRoot_Then_PromoteLeftChildToRemovedNodesPlace()
        {
            // Arrange
            //       D
            //     /   \
            //    B     H
            //   /     /
            //  A     F
            //       / \
            //      E   G

            var sut = CreateSut()
                        .Add(4, "D")
                        .Add(2, "B")
                        .Add(1, "A")
                        .Add(8, "H")
                        .Add(6, "F")
                        .Add(5, "E")
                        .Add(7, "G");

            // Act
            sut = sut.Delete(2);

            // Assert
            //       D
            //     /   \
            //    A     H
            //         /
            //        F
            //       / \
            //      E   G

            sut.AssertRootEqual(4, "D");
            sut.Root.Left.AssertLeafEqual(1, "A");
            sut.Root.Right.AssertBranchEqual(8, "H");
            sut.Root.Right.Left.AssertBranchEqual(6, "F");
            sut.Root.Right.Left.Left.AssertLeafEqual(5, "E");
            sut.Root.Right.Left.Right.AssertLeafEqual(7, "G");
        }

        [Fact]
        public void Given_Tree_When_DeletingNodeRightChildThatHasNoLeft_From_RightOfRoot_Then_PromoteRightChildToReplaceRemovedNode()
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
            sut = sut.Delete(6);

            // Assert
            //       D
            //     /   \
            //    B     G
            //   / \   / \
            //  A   C E   H

            sut.AssertRootEqual(4, "D");
            sut.Root.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Right.AssertLeafEqual(3, "C");
            sut.Root.Right.AssertBranchEqual(7, "G");
            sut.Root.Right.Left.AssertLeafEqual(5, "E");
            sut.Root.Right.Right.AssertLeafEqual(8, "H");
        }

        [Fact]
        public void Given_Tree_When_DeletingNodeRightChildThatHasNoLeft_From_LeftOfRoot_Then_PromoteRightChildToReplaceRemovedNode()
        {
            // Arrange
            //       I
            //     /   \
            //    F     J
            //   / \  
            //  E   G 
            //       \
            //        H

            var sut = CreateSut()
                        .Add(9, "I")
                        .Add(6, "F")
                        .Add(5, "E")
                        .Add(7, "G")
                        .Add(8, "H")
                        .Add(10, "J");

            // Act
            sut = sut.Delete(6);

            // Assert
            //       I
            //     /   \
            //    G     J
            //   / \  
            //  E   H 

            sut.AssertRootEqual(9, "I");
            sut.Root.Left.AssertBranchEqual(7, "G");
            sut.Root.Left.Left.AssertLeafEqual(5, "E");
            sut.Root.Left.Right.AssertLeafEqual(8, "H");
            sut.Root.Right.AssertLeafEqual(10, "J");

        }

        [Fact]
        public void Given_Tree_When_DeletingNodeWithRightChildThatHasLeftChild_From_RightOfRoot_Then_PromoteRightChildLeftMostChildAsReplacement()
        {
            // Arrange
            //       D
            //     /   \
            //    B     F
            //   / \   / \
            //  A   C E   H
            //           /
            //          G

            var sut = CreateSut()
                        .Add(4, "D")
                        .Add(2, "B")
                        .Add(1, "A")
                        .Add(3, "C")
                        .Add(6, "F")
                        .Add(5, "E")
                        .Add(8, "H")
                        .Add(7, "G");

            // Act
            sut = sut.Delete(6);

            // Assert
            //       D
            //     /   \
            //    B     G
            //   / \   / \
            //  A   C E   H

            sut.AssertRootEqual(4, "D");
            sut.Root.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Right.AssertLeafEqual(3, "C");
            sut.Root.Right.AssertBranchEqual(7, "G");
            sut.Root.Right.Left.AssertLeafEqual(5, "E");
            sut.Root.Right.Right.AssertLeafEqual(8, "H");
            Assert.Null(sut.Root.Right.Right.Left);
        }

        [Fact]
        public void Given_Tree_When_DeletingNodeWithRightChildThatHasLeftChild_And_RightChild_From_RightOfRoot_Then_PromoteRightChildLeftMostChildAsReplacement()
        {
            // Arrange
            //       D
            //     /   \
            //    B     F
            //   / \   / \
            //  A   C E   I
            //           /
            //          G
            //           \
            //            H

            var sut = CreateSut()
                        .Add(4, "D")
                        .Add(2, "B")
                        .Add(1, "A")
                        .Add(3, "C")
                        .Add(6, "F")
                        .Add(5, "E")
                        .Add(9, "I")
                        .Add(7, "G")
                        .Add(8, "H");

            // Act
            sut = sut.Delete(6);

            // Assert
            //       D
            //     /   \
            //    B     G
            //   / \   / \
            //  A   C E   I
            //           /
            //          H

            sut.AssertRootEqual(4, "D");
            sut.Root.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Right.AssertLeafEqual(3, "C");
            sut.Root.Right.AssertBranchEqual(7, "G");
            sut.Root.Right.Left.AssertLeafEqual(5, "E");
            sut.Root.Right.Right.AssertBranchEqual(9, "I");
            sut.Root.Right.Right.Left.AssertLeafEqual(8, "H");
        }

        [Fact]
        public void Given_Tree_When_DeletingNodeWithRightChildThatHasLeftChildNestedTwoLevels_And_RightChild_From_RightOfRoot_Then_PromoteRightChildLeftMostChildAsReplacement()
        {
            // Arrange
            //       D
            //     /   \
            //    B     F
            //   / \   / \
            //  A   C E   J
            //           /
            //          I
            //         /
            //        G
            //         \
            //          H

            var sut = CreateSut()
                        .Add(4, "D")
                        .Add(2, "B")
                        .Add(1, "A")
                        .Add(3, "C")
                        .Add(6, "F")
                        .Add(5, "E")
                        .Add(10, "J")
                        .Add(9, "I")
                        .Add(7, "G")
                        .Add(8, "H");

            // Act
            sut = sut.Delete(6);

            // Assert
            //       D
            //     /   \
            //    B     G
            //   / \   / \
            //  A   C E   J
            //           /
            //          I
            //         /
            //        H

            sut.AssertRootEqual(4, "D");
            sut.Root.Left.AssertBranchEqual(2, "B");
            sut.Root.Left.Left.AssertLeafEqual(1, "A");
            sut.Root.Left.Right.AssertLeafEqual(3, "C");
            sut.Root.Right.AssertBranchEqual(7, "G");
            sut.Root.Right.Left.AssertLeafEqual(5, "E");
            sut.Root.Right.Right.AssertBranchEqual(10, "J");
            sut.Root.Right.Right.Left.AssertBranchEqual(9, "I");
            sut.Root.Right.Right.Left.Left.AssertLeafEqual(8, "H");
        }

        [Fact]
        public void Given_Tree_When_DeletingNodeWithRightChildThatHasLeftChild_From_LeftOfRoot_Then_PromoteRightChildLeftMostChildAsReplacement()
        {
            // Arrange
            //       I
            //     /   \
            //    E     J
            //   / \ 
            //  D   H
            //     /
            //    F
            //     \
            //      G

            var sut = CreateSut()
                        .Add(9, "I")
                        .Add(10, "J")
                        .Add(5, "E")
                        .Add(4, "D")
                        .Add(8, "H")
                        .Add(6, "F")
                        .Add(7, "G");

            // Act
            sut = sut.Delete(5);

            // Assert
            //       I
            //     /   \
            //    F     J
            //   / \ 
            //  D   H
            //     /
            //    G

            sut.AssertRootEqual(9, "I");
            sut.Root.Right.AssertLeafEqual(10, "J");
            sut.Root.Left.AssertBranchEqual(6, "F");
            sut.Root.Left.Left.AssertLeafEqual(4, "D");
            sut.Root.Left.Right.AssertBranchEqual(8, "H");
            sut.Root.Left.Right.Left.AssertLeafEqual(7, "G");
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
            var result = sut.Find(9);

            // Assert
            Assert.Empty(result);
        }

        #endregion
    }
}
