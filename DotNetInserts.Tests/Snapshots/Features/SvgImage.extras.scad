$fn=48;
translate([0,0,0])
{
translate(v = [30.00, 30.00, 0.00])
{
    translate(v = [20.00, 10.00, 0.00])
    {
        mirror([1.00, 0.00, 0.00])
        {
            resize([40.00, 20.00, 0.60])
            {
                linear_extrude(height = 0.60)
                offset(delta=0.001)
                import("Logo.svg", center = true);
            }
        }
    }
}

}
translate([110,0,0])
{
translate(v = [35.00, 32.50, 1.60])
{
    translate(v = [15.00, 7.50, 0.00])
    {
        resize([30.00, 15.00, 0.40])
        {
            linear_extrude(height = 0.40)
            offset(delta=0.001)
            import("Logo.svg", center = true);
        }
    }
}

}
