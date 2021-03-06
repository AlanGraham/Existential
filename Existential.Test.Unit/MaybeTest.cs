﻿// <copyright file="MaybeTest.cs" company="Gavin Greig">
//     Copyright (c) Dr. Gavin T.D. Greig, 2020.
// </copyright>

namespace Existential.Test
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public static class MaybeTest
    {
        [Test]
        public static void Maybe_WithValue_Succeeds()
        {
            // Act
            var theMaybe = Maybe<string>.WithValue("Success");

            // Assert
            Assert.That(theMaybe.GetValueOr("Fail"), Is.EqualTo("Success"));
        }

        [Test]
        public static void Maybe_WithValue_FailsAsExpected()
        {
            // Act
            var theMaybe = Maybe<string>.WithValue(null);

            // Assert
            Assert.That(theMaybe.GetValueOr("Fail"), Is.EqualTo("Fail"));
        }

        [Test]
        public static void Maybe_WithGuaranteedValue_Succeeds()
        {
            // Act
            var theMaybe = Maybe<string>.WithGuaranteedValue("Success");

            // Assert
            Assert.That(theMaybe.GetValueOr("Fail"), Is.EqualTo("Success"));
        }

        [Test]
        public static void Maybe_WithGuaranteedValue_FailsAsExpected()
        {
            // Assert
            Assert.That(
                () =>
                {
                    // Act
#pragma warning disable S1481 // Unused local variables should be removed
                    var theMaybe = Maybe<string>.WithGuaranteedValue(null);
#pragma warning restore S1481 // Unused local variables should be removed
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null."));
        }

        [Test]
        public static void Maybe_OrWithValue_ReturnsOriginal()
        {
            // Arrange
            Maybe<string> theOriginalMaybe = "Success";
            Maybe<string> theAlternativeMaybe = "Fail";

            // Act
            Maybe<string> theResult = theOriginalMaybe.Or(theAlternativeMaybe);

            // Assert
            Assert.That(theResult.GetValueOr("Unexpected Fail!"), Is.EqualTo("Success"));
        }

        [Test]
        public static void Maybe_OrWithNull_ReturnsAlternative()
        {
            // Arrange
            Maybe<string> theOriginalMaybe = null;
            Maybe<string> theAlternativeMaybe = "Success";

            // Act
            Maybe<string> theResult = theOriginalMaybe.Or(theAlternativeMaybe);

            // Assert
            Assert.That(theResult.GetValueOr("Unexpected Fail!"), Is.EqualTo("Success"));
        }

        [Test]
        public static void Maybe_OrFunctionWithValue_ReturnsOriginal()
        {
            // Arrange
            Maybe<string> theOriginalMaybe = "Success";
            Maybe<string> theAlternativeMaybe = "Fail";

            // Act
            Maybe<string> theResult = theOriginalMaybe.Or(() => theAlternativeMaybe);

            // Assert
            Assert.That(theResult.GetValueOr("Unexpected Fail!"), Is.EqualTo("Success"));
        }

        [Test]
        public static void Maybe_OrFunctionWithNull_ReturnsAlternative()
        {
            // Arrange
            Maybe<string> theOriginalMaybe = null;
            Maybe<string> theAlternativeMaybe = "Success";

            // Act
            Maybe<string> theResult = theOriginalMaybe.Or(() => theAlternativeMaybe);

            // Assert
            Assert.That(theResult.GetValueOr("Unexpected Fail!"), Is.EqualTo("Success"));
        }

        [TestCase("A non-null string")]
        public static void Maybe_IsRecognisedAsNullCheck(string inText)
        {
            // Arrange
            // ReSharper disable once UnusedVariable
#pragma warning disable IDE0059, S1481 // Unnecessary assignment of a value, unused local variable
            Maybe<string> theMaybe = inText;
#pragma warning restore IDE0059, S1481 // Unnecessary assignment of a value, unused local variable

            // Act
            // Without assignment to Maybe, this should raise a CA1062 warning.
            // Wouldn't usually do this, because it could cause a null deference,
            // but it confirms that Maybe configuration in .editorconfig is correct.
            int theLength = inText.Length;

            // Assert
            Assert.That(theLength, Is.EqualTo(17));
        }

        [Test]
        public static void Apply_WithNoValue_ReturnsMaybeWithNoValue()
        {
            // Arrange
            Maybe<string> theEmptyMaybe = null;

            // Act
            Maybe<int> theResult = theEmptyMaybe.Apply(
                inText =>
                {
                    Maybe<int> theIntegerMaybe = inText.Length;
                    return theIntegerMaybe;
                });

            // Assert
            Assert.That(theResult.GetValueOr(12), Is.EqualTo(12));
        }

        [Test]
        public static void Bind_WithNoValue_ReturnsMaybeWithNoValue()
        {
            // Arrange
            Maybe<string> theEmptyMaybe = null;

            // Act
            Maybe<int> theResult = theEmptyMaybe.Bind(
                inText =>
                {
                    Maybe<int> theIntegerMaybe = inText.Length;
                    return theIntegerMaybe;
                });

            // Assert
            Assert.That(theResult.GetValueOr(12), Is.EqualTo(12));
        }

        [Test]
        public static void Apply_WithValue_ReturnsExpectedValue()
        {
            // Arrange
            Maybe<string> theTextMaybe = "Call me Maybe";

            // Act
            Maybe<int> theResult = theTextMaybe.Apply(
                inText =>
                {
                    Maybe<int> theIntegerMaybe = inText.Length;
                    return theIntegerMaybe;
                });

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(13));
        }

        [Test]
        public static void Bind_WithValue_ReturnsExpectedValue()
        {
            // Arrange
            Maybe<string> theTextMaybe = "Call me Maybe";

            // Act
            Maybe<int> theResult = theTextMaybe.Bind(
                inText =>
                {
                    Maybe<int> theIntegerMaybe = inText.Length;
                    return theIntegerMaybe;
                });

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(13));
        }

        [Test]
        public static void DefaultCreate_WithInt_HasNoValue()
        {
            // Act
            var theResult = default(Maybe<int>);

            // Assert
            Assert.That(theResult.GetValueOr(1), Is.EqualTo(1));
        }

        [Test]
        public static void DefaultCreate_WithInt_Succeeds()
        {
            // Act
            var theResult = default(Maybe<int>);

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<int>>());
        }

        [Test]
        public static void DefaultCreate_WithString_HasNoValue()
        {
            // Act
            var theResult = default(Maybe<string>);

            // Assert
            Assert.That(theResult.GetValueOr("Default value"), Is.EqualTo("Default value"));
        }

        [Test]
        public static void DefaultCreate_WithString_Succeeds()
        {
            // Act
            var theResult = default(Maybe<string>);

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<string>>());
        }

        [Test]
        public static void DoesNotEqualOperator_WithDifferentValues_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = "A different string";

            // Act
            bool theResult = theFirstMaybe != theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void DoesNotEqualOperator_WithIdenticalValues_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = "A test string";

            // Act
            bool theResult = theFirstMaybe != theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void DoesNotEqualOperator_WhereOneValueDoesNotExist_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = null;

            // Act
            bool theResult = theFirstMaybe != theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void DoesNotEqualOperator_WhereNeitherValueExists_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theFirstMaybe = null;
            Maybe<string> theSecondMaybe = null;

            // Act
            bool theResult = theFirstMaybe != theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void EqualsOperator_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = "A different string";

            // Act
            bool theResult = theFirstMaybe == theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void EqualsOperator_WithIdenticalValues_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = "A test string";

            // Act
            bool theResult = theFirstMaybe == theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void EqualsOperator_WhereOneValueDoesNotExist_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "A test string";
            Maybe<string> theSecondMaybe = null;

            // Act
            bool theResult = theFirstMaybe == theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void Equals_WithDifferentTypes_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "2";
            Maybe<int> theSecondMaybe = 2;

            // Act
            // ReSharper disable once SuspiciousTypeConversion.Global
            bool theResult = theFirstMaybe.Equals(theSecondMaybe);

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void Equals_WithForcedObjectComparison_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "Test";
            Maybe<string> theSecondMaybe = "Test";

            // Act
            bool theResult = theFirstMaybe.Equals((object)theSecondMaybe);

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void Equals_WithForcedObjectComparisonAndDifferentTypes_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theFirstMaybe = "Test";
            Maybe<int> theSecondMaybe = 2;

            // Act
            // ReSharper disable once SuspiciousTypeConversion.Global
            bool theResult = theFirstMaybe.Equals(theSecondMaybe);

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void EqualsOperator_WhenNeitherValueExists_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theFirstMaybe = null;
            Maybe<string> theSecondMaybe = null;

            // Act
            bool theResult = theFirstMaybe == theSecondMaybe;

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void HashCode_WithInvalid_HasExpectedValue()
        {
            // Act
            Maybe<string> theResult = null;

            // Assert
            Assert.That(theResult.GetHashCode(), Is.EqualTo(0));
        }

        [Test]
        public static void HashCode_WithValid_HasExpectedValue()
        {
            // Arrange
            string theValidString = "A valid string";

            // Act
            Maybe<string> theResult = theValidString;

            // Assert
            Assert.That(
                theResult.GetHashCode(),
                Is.EqualTo(theValidString.GetHashCode(StringComparison.Ordinal)));
        }

        [Test]
        public static void ImplicitCreate_WithMaybe_ReturnsSameValue()
        {
            // Arrange
            Maybe<int> theOriginalMaybe = 1;

            // Act
            Maybe<int> theResult = theOriginalMaybe;

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void ImplicitCreate_WithInt_HasExpectedValue()
        {
            // Act
            Maybe<int> theResult = 1;

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void ImplicitCreate_WithInt_Succeeds()
        {
            // Act
            Maybe<int> theResult = 1;

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<int>>());
        }

        [Test]
        public static void ImplicitCreate_WithString_HasExpectedValue()
        {
            // Act
            Maybe<string> theResult = "Test";

            // Assert
            Assert.That(theResult.GetValueOr(string.Empty), Is.EqualTo("Test"));
        }

        [Test]
        public static void ImplicitCreate_WithString_Succeeds()
        {
            // Act
            Maybe<string> theResult = "Test";

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<string>>());
        }

        [Test]
        public static void Match_WithFuncAndEmpty_AppliesAlternativeFunc()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            int theResult = theMaybe.Match(inText => inText.Length, () => -1).GetValueOr(-2);

            // Assert
            Assert.That(theResult, Is.EqualTo(-1));
        }

        [Test]
        public static void DoEither_WithFuncAndEmpty_AppliesAlternativeFunc()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            int theResult = theMaybe.DoEither(inText => inText.Length, () => -1).GetValueOr(-2);

            // Assert
            Assert.That(theResult, Is.EqualTo(-1));
        }

        [Test]
        public static void Match_WithFuncAndValue_AppliesExpectedFunc()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            int theResult = theMaybe.Match(inText => inText.Length, () => -1).GetValueOr(-2);

            // Assert
            Assert.That(theResult, Is.EqualTo(13));
        }

        [Test]
        public static void DoEither_WithFuncAndValue_AppliesExpectedFunc()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            int theResult = theMaybe.DoEither(inText => inText.Length, () => -1).GetValueOr(-2);

            // Assert
            Assert.That(theResult, Is.EqualTo(13));
        }

        [Test]
        public static void Match_WithFuncAndEdgeCase_AppliesExpectedFunc()
        {
            // Arrange
            Maybe<string> theMaybe = string.Empty;

            // Act
            int theResult = theMaybe.Match(inText => inText.Length, () => -1).GetValueOr(-2);

            // Assert
            Assert.That(theResult, Is.EqualTo(0));
        }

        [Test]
        public static void DoEither_WithFuncAndEdgeCase_AppliesExpectedFunc()
        {
            // Arrange
            Maybe<string> theMaybe = string.Empty;

            // Act
            int theResult = theMaybe.DoEither(inText => inText.Length, () => -1).GetValueOr(-2);

            // Assert
            Assert.That(theResult, Is.EqualTo(0));
        }

        [Test]
        public static void Match_WithActionAndEmpty_AppliesAlternativeAction()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";
            bool isActionApplied = false;
            void Action(string inText) => isActionApplied = inText == "A test string";
            void AlternativeAction() => isActionApplied = false;

            // Act
            theMaybe.Match(Action, AlternativeAction);

            // Assert
            Assert.That(isActionApplied, Is.True);
        }

        [Test]
        public static void DoEither_WithActionAndEmpty_AppliesAlternativeAction()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";
            bool isActionApplied = false;
            void Action(string inText) => isActionApplied = inText == "A test string";
            void AlternativeAction() => isActionApplied = false;

            // Act
            theMaybe.DoEither(Action, AlternativeAction);

            // Assert
            Assert.That(isActionApplied, Is.True);
        }

        [Test]
        public static void Match_WithActionAndValue_AppliesExpectedAction()
        {
            // Arrange
            Maybe<string> theMaybe = null;
            bool isAlternativeActionApplied = false;
            void Action(string inText) => isAlternativeActionApplied = inText == "A test string";
            void AlternativeAction() => isAlternativeActionApplied = true;

            // Act
            theMaybe.Match(Action, AlternativeAction);

            // Assert
            Assert.That(isAlternativeActionApplied, Is.True);
        }

        [Test]
        public static void DoEither_WithActionAndValue_AppliesExpectedAction()
        {
            // Arrange
            Maybe<string> theMaybe = null;
            bool isAlternativeActionApplied = false;
            void Action(string inText) => isAlternativeActionApplied = inText == "A test string";
            void AlternativeAction() => isAlternativeActionApplied = true;

            // Act
            theMaybe.DoEither(Action, AlternativeAction);

            // Assert
            Assert.That(isAlternativeActionApplied, Is.True);
        }

        [Test]
        public static void IfExists_WithActionAndNull_DoesNotApplyAction()
        {
            // Arrange
            Maybe<string> theMaybe = null;
            bool isActionApplied = false;
            void Action(string inText) => isActionApplied = inText == null;

            // Act
            theMaybe.IfExists(Action);

            // Assert
            Assert.That(isActionApplied, Is.False);
        }

        [Test]
        public static void IfExists_WithActionAndValue_AppliesAction()
        {
            // Arrange
            Maybe<string> theMaybe = "Test";
            bool isActionApplied = false;
            void Action(string inText) => isActionApplied = inText == "Test";

            // Act
            theMaybe.IfExists(Action);

            // Assert
            Assert.That(isActionApplied, Is.True);
        }

        [Test]
        public static void Some_WithInt_HasExpectedValue()
        {
            // Act
            var theResult = Maybe.Some(1);

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(1));
        }

        [Test]
        public static void Some_WithInt_Succeeds()
        {
            // Act
            var theResult = Maybe.Some(1);

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<int>>());
        }

        [Test]
        public static void Some_WithString_HasExpectedValue()
        {
            // Act
            var theResult = Maybe.Some("Test");

            // Assert
            Assert.That(theResult.GetValueOr(string.Empty), Is.EqualTo("Test"));
        }

        [Test]
        public static void Some_WithString_Succeeds()
        {
            // Act
            var theResult = Maybe.Some("Test");

            // Assert
            Assert.That(theResult, Is.InstanceOf<Maybe<string>>());
        }

        [Test]
        public static void Some_WithMaybe_IsMaybe()
        {
            // Arrange
            string theValidString = "A valid string";
            Maybe<string> theMaybe = theValidString;

            // Act
            var theResult = Maybe.Some(theMaybe);

            // Assert
            Assert.That(theResult.GetValueOr("A default string"), Is.EqualTo("A valid string"));
        }

        [Test]
        public static void Some_WithNone_IsNone()
        {
            // Arrange
            Maybe<string> theNone = null;

            // Act
            var theResult = Maybe.Some(theNone);

            // Assert
            Assert.That(theResult.GetValueOr("A default string"), Is.EqualTo("A default string"));
        }

        [Test]
        public static void Some_WithNull_IsNone()
        {
            // Act
            var theResult = Maybe.Some((string)null);

            // Assert
            Assert.That(theResult.GetValueOr("A default string"), Is.EqualTo("A default string"));
        }

        [Test]
        public static void ApplyOverload_WithNoValue_ReturnsMaybeWithNoValue()
        {
            // Arrange
            Maybe<string> theNullString = null;

            // Act
            Maybe<int> theResult = theNullString.Apply(inText => inText.Length);

            // Assert
            Assert.That(theResult.GetValueOr(17), Is.EqualTo(17));
        }

        [Test]
        public static void Map_WithNoValue_ReturnsMaybeWithNoValue()
        {
            // Arrange
            Maybe<string> theNullString = null;

            // Act
            Maybe<int> theResult = theNullString.Map(inText => inText.Length);

            // Assert
            Assert.That(theResult.GetValueOr(17), Is.EqualTo(17));
        }

        [Test]
        public static void ApplyOverload_WithValue_ReturnsExpectedValue()
        {
            // Arrange
            Maybe<string> theString = "Call me Maybe";

            // Act
            Maybe<int> theResult = theString.Apply(inText => inText.Length);

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(13));
        }

        [Test]
        public static void Map_WithValue_ReturnsExpectedValue()
        {
            // Arrange
            Maybe<string> theString = "Call me Maybe";

            // Act
            Maybe<int> theResult = theString.Map(inText => inText.Length);

            // Assert
            Assert.That(theResult.GetValueOr(0), Is.EqualTo(13));
        }

        [Test]
        public static void ToMaybe_WithMaybe_IsMaybe()
        {
            // Arrange
            var theDefaultMaybe = default(Maybe<string>);
            var theMaybeWithValue = theDefaultMaybe.ToMaybe("A test string");

            // Act
            var theResult = theDefaultMaybe.ToMaybe(theMaybeWithValue);

            // Assert
            Assert.That(theResult, Is.EqualTo(theMaybeWithValue));
        }

        [Test]
        public static void ToMaybe_WithNone_IsNone()
        {
            // Arrange
            var theDefaultMaybe = default(Maybe<string>);

            // Act
            var theResult = theDefaultMaybe.ToMaybe(theDefaultMaybe);

            // Assert
            Assert.That(theResult, Is.EqualTo(theDefaultMaybe));
        }

        [Test]
        public static void ToMaybe_WithNull_IsNone()
        {
            // Arrange
            var theDefaultMaybe = default(Maybe<string>);

            // Act
            var theResult = theDefaultMaybe.ToMaybe(null);

            // Assert
            Assert.That(theResult, Is.EqualTo(theDefaultMaybe));
        }

        [Test]
        public static void ToMaybe_WithValue_ContainsValue()
        {
            // Arrange
            string theTestValue = "A test string";
            var theDefaultMaybe = default(Maybe<string>);

            // Act
            var theResult = theDefaultMaybe.ToMaybe(theTestValue);

            // Assert
            Assert.That(theResult.GetValueOr("A default string"), Is.EqualTo(theTestValue));
        }

        [Test]
        public static void ToString_WithNullString_ReturnsStringOfDefault()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            string theResult = theMaybe.ToString();

            // Assert
            Assert.That(theResult, Is.EqualTo(string.Empty));
        }

        [Test]
        public static void ToString_WithValue_ReturnsStringOfValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.ToString();

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void TryGetValue_WithNoValue_HasDefaultOutValue()
        {
            // Arrange
            Maybe<string> theTestValue = null;

            // Act
            _ = theTestValue.TryGetValue(out string theOutValue);

            // Assert
            Assert.That(theOutValue, Is.EqualTo(default(string)));
        }

        [Test]
        public static void TryGetValue_WithNoValue_ReturnsFalse()
        {
            // Arrange
            Maybe<string> theTestValue = null;

            // Act
            bool theResult = theTestValue.TryGetValue(out _);

            // Assert
            Assert.That(theResult, Is.False);
        }

        [Test]
        public static void TryGetValue_WithValue_HasCorrectOutValue()
        {
            // Arrange
            Maybe<string> theTestValue = "A test string";

            // Act
            _ = theTestValue.TryGetValue(out string theOutValue);

            // Assert
            Assert.That(theOutValue, Is.EqualTo("A test string"));
        }

        [Test]
        public static void TryGetValue_WithValue_ReturnsTrue()
        {
            // Arrange
            Maybe<string> theTestValue = "A test string";

            // Act
            bool theResult = theTestValue.TryGetValue(out _);

            // Assert
            Assert.That(theResult, Is.True);
        }

        [Test]
        public static void GetValueOr_WithNull_ReturnsDefaultValue()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            string theResult = theMaybe.GetValueOr("A default string");

            // Assert
            Assert.That(theResult, Is.EqualTo("A default string"));
        }

        [Test]
        public static void GetValueOr_WithNullAndNullDefault_ThrowsException()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
#pragma warning disable S1481 // Unused local variables should be removed
                    string theResult = theMaybe.GetValueOr((string)null);
#pragma warning restore S1481 // Unused local variables should be removed
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null")
                    .And.Message.Contains("inDefaultValue"));
        }

        [Test]
        public static void GetValueOr_WithNullAndFactory_ReturnsFactoryValue()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Act
            string theResult = theMaybe.GetValueOr(() => "A factory string");

            // Assert
            Assert.That(theResult, Is.EqualTo("A factory string"));
        }

        [Test]
        public static void GetValueOr_WithNullAndNullDefaultFactory_ThrowsException()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
#pragma warning disable S1481 // Unused local variables should be removed
                    string theResult = theMaybe.GetValueOr((Func<string>)null);
#pragma warning restore S1481 // Unused local variables should be removed
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null")
                    .And.Message.Contains("inDefaultValueFactory"));
        }

        [Test]
        public static void GetValueOr_WithValue_ReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.GetValueOr("A default string");

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void GetValueOr_WithValueAndNullDefault_StillReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.GetValueOr((string)null);

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void GetValueOr_WithValueAndFactory_ReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.GetValueOr(() => "A factory string");

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void GetValueOr_WithValueAndNullFactory_StillReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.GetValueOr((Func<string>)null);

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void GetValueOrMaybe_WithNoneAndAlternativeMaybe_ReturnsAlternativeMaybe()
        {
            // Arrange
            Maybe<string> theDefault = "A different default string";
            Maybe<string> theMaybe = null;

            // Act
            Maybe<string> theResult = theMaybe.GetValueOrMaybe(theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theDefault));
        }

        [Test]
        public static void GetValueOrMaybe_WithNoneAndEmptyAlternativeMaybe_ReturnsAlternativeMaybe()
        {
            // Arrange
            Maybe<string> theDefault = null;
            Maybe<string> theMaybe = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
#pragma warning disable S1481 // Unused local variables should be removed
                    Maybe<string> theResult = theMaybe.GetValueOrMaybe(theDefault);
#pragma warning restore S1481 // Unused local variables should be removed
                },
                Throws.ArgumentException
                    .With.Message
                    .Contains("An empty Maybe<T> cannot be provided as the default value to replace an empty Maybe<T>.")
                    .And.Message.Contains("inAlternativeValue"));
        }

        [Test]
        public static void GetValueOrMaybe_WithNoneAndMaybeFactory_ReturnsFactoryValue()
        {
            // Arrange
            Maybe<string> theDefault = "A factory string";
            Maybe<string> theMaybe = null;

            // Act
            Maybe<string> theResult = theMaybe.GetValueOrMaybe(() => theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theDefault));
        }

        [Test]
        public static void GetValueOrMaybe_WithNoneAndNullMaybeFactory_ReturnsFactoryValue()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
#pragma warning disable S1481 // Unused local variables should be removed
                    Maybe<string> theResult = theMaybe.GetValueOrMaybe((Func<Maybe<string>>)null);
#pragma warning restore S1481 // Unused local variables should be removed
                },
                Throws.ArgumentNullException
                    .With.Message.Contains("Value cannot be null")
                    .And.Message.Contains("inAlternativeValueFactory"));
        }

        [Test]
        public static void GetValueOrMaybe_WithValueAndAlternativeMaybe_ReturnsValue()
        {
            // Arrange
            Maybe<string> theDefault = "A different default string";
            Maybe<string> theMaybe = "A test string";

            // Act
            Maybe<string> theResult = theMaybe.GetValueOrMaybe(theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theMaybe));
        }

        [Test]
        public static void GetValueOrMaybe_WithValueAndNullAlternativeMaybe_StillReturnsValue()
        {
            // Arrange
            Maybe<string> theDefault = null;
            Maybe<string> theMaybe = "A test string";

            // Act
            Maybe<string> theResult = theMaybe.GetValueOrMaybe(theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theMaybe));
        }

        [Test]
        public static void GetValueOrMaybe_WithValueAndMaybeFactory_ReturnsValue()
        {
            // Arrange
            Maybe<string> theDefault = "A factory string";
            Maybe<string> theMaybe = "A test string";

            // Act
            Maybe<string> theResult = theMaybe.GetValueOrMaybe(() => theDefault);

            // Assert
            Assert.That(theResult, Is.EqualTo(theMaybe));
        }

        [Test]
        public static void GetValueOrMaybe_WithValueAndNullMaybeFactory_StillReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            Maybe<string> theResult = theMaybe.GetValueOrMaybe((Func<Maybe<string>>)null);

            // Assert
            Assert.That(theResult, Is.EqualTo(theMaybe));
        }

        [Test]
        public static void GetValueOrThrow_WithNone_Throws()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    _ = theMaybe.GetValueOrThrow("An error message.");
                },
                Throws.InvalidOperationException.With.Message.Contains("An error message"));
        }

        [Test]
        public static void GetValueOrThrow_WithNoneAndNoMessage_StillThrows()
        {
            // Arrange
            Maybe<string> theMaybe = null;

            // Assert
            Assert.That(
                () =>
                {
                    // Act
                    _ = theMaybe.GetValueOrThrow(null);
                },
                Throws.InvalidOperationException);
        }

        [Test]
        public static void GetValueOrThrow_WithValue_ReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.GetValueOrThrow("An error message.");

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }

        [Test]
        public static void GetValueOrThrow_WithValueAndNoMessage_StillReturnsValue()
        {
            // Arrange
            Maybe<string> theMaybe = "A test string";

            // Act
            string theResult = theMaybe.GetValueOrThrow(null);

            // Assert
            Assert.That(theResult, Is.EqualTo("A test string"));
        }
    }
}