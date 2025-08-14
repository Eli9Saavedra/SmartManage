using SmartManage.Components.Data;
using SmartManage.Components.Models;
using Microsoft.EntityFrameworkCore;

public class RecordService
{
    private readonly SmartManageContext _context;

    public RecordService(SmartManageContext context)
    {
        _context = context;
    }

    public async Task AddRecordAsync(Record record)
    {
        _context.Records.Add(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRecordAsync(Record record)
    {
        _context.Records.Update(record);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRecordAsync(int recordId)
    {
        var record = await _context.Records.FindAsync(recordId);
        if (record != null)
        {
            _context.Records.Remove(record);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Record>> GetAllRecordsAsync()
    {
        return await _context.Records.ToListAsync();
    }
}