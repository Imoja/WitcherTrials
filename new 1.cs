public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
{
    FieldInfo defInfo = AccessTools.Field(type: typeof(Thing), name: nameof(Thing.def));

    CodeInstruction[] codeInstructions = instructions as CodeInstruction[] ?? instructions.ToArray();
    for (int index = 0; index < codeInstructions.Length; index++)
    {
        CodeInstruction instruction = codeInstructions[index];
        if (instruction.opcode == OpCodes.Ldfld && instruction.operand == defInfo)
        {
            index += 1;
            instruction = new CodeInstruction(opcode: OpCodes.Call, operand: AccessTools.Method(type: typeof(Debug), name: nameof(CreateRecipes)));
        }
        yield return instruction;
    }
}

public static List<RecipeDef> CreateRecipes(Pawn p)
{
    List<RecipeDef> recipes = p.def.AllRecipes;

    return recipes;
}
harmony.Patch(original: typeof(HealthCardUtility).GetNestedTypes(bindingAttr: AccessTools.all).First(predicate: t => t.GetMethods(bindingAttr: AccessTools.all).Any(predicate: mi => mi.ReturnType == typeof(List<FloatMenuOption>))).GetMethods(bindingAttr: AccessTools.all).First(predicate: mi => mi.ReturnType == typeof(List<FloatMenuOption>)), transpiler: new HarmonyMethod(type: typeof(Debug), name: nameof(Transpiler)));

----------------------------------------------------------------------------------------------------------

public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
{
    FieldInfo defInfo = AccessTools.Field(type: typeof(Thing), name: nameof(Thing.def));

    CodeInstruction[] codeInstructions = instructions as CodeInstruction[] ?? instructions.ToArray();
    for (int index = 0; index < codeInstructions.Length; index++)
    {
        CodeInstruction instruction = codeInstructions[index];
        if (instruction.opcode == OpCodes.Ldfld && instruction.operand == defInfo)
        {
            index += 1;
            instruction = new CodeInstruction(opcode: OpCodes.Call, operand: AccessTools.Method(type: typeof(YOURTYPE), name: nameof(CreateRecipes)));
        }
        yield return instruction;
    }
}

public static List<RecipeDef> CreateRecipes(Pawn p)
{
    List<RecipeDef> recipes = p.def.AllRecipes;

    return recipes;
}

harmony.Patch(original: typeof(HealthCardUtility).GetNestedTypes(bindingAttr: AccessTools.all).First(predicate: t => t.GetMethods(bindingAttr: AccessTools.all).Any(predicate: mi => mi.ReturnType == typeof(List<FloatMenuOption>))).GetMethods(bindingAttr: AccessTools.all).First(predicate: mi => mi.ReturnType == typeof(List<FloatMenuOption>)), transpiler: new HarmonyMethod(type: typeof(YOURTYPE), name: nameof(Transpiler)));

------------------------------------------------------------------------------------
Fil's Fix:

AccessTools.Method(typeof(HealthCardUtility), “GenerateSurgeryOption”) should be all you need should there be only 1 method in that class with that name, pls confirm