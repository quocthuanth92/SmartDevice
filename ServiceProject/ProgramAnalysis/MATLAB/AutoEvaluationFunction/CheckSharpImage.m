function [ Result, Rate, RunningTime, ErrorStatus ] = CheckSharpImage( ImagePath )
%ANALYSISIMAGEBLURORNOT Summary of this function goes here
%   Detailed explanation goes here
    ErrorStatus = ' ';
    
    try
        I = imread(ImagePath);
        
        tic
        % LAPD: Diagonal Laplacian (Thelen2009)
        FocusMeasure = fmeasure(I,'LAPD');
        RunningTime = toc;

        % The optimal threshold is 4.0
        if(FocusMeasure  < 4.0)
            isNotBlur = 0;
        else 
            isNotBlur = 1;
        end
        
        %Convert to Rate 0 - 100
        n1 = 0;
        n2 = 4;
        n3 = 20;
        CheckedValue = FocusMeasure;
        if(CheckedValue > n3)
            CheckedValue = n3;
        end
        if(CheckedValue > n2)
            PercentValue = 0.5 + (CheckedValue - n2) / (2 * (n3 - n2));
        else
            PercentValue = (CheckedValue - n1) / (2 * (n2 - n1));
        end
        NotBlurRate = PercentValue;
        
        Result = isNotBlur;
        Rate = NotBlurRate;
%         OutParam1 = FocusMeasure;
%         OutParam2 = 0;
%         OutParam3 = 0;
        
    catch ME 
        ErrorStatus = ['Error in check Blur or Not:' ME.message ' for ' ImagePath];   
        Result = 0;
        Rate = 0;
        RunningTime = 0;
        
%         OutParam1 = 0;
%         OutParam2 = 0;
%         OutParam3 = 0;
    end

end

